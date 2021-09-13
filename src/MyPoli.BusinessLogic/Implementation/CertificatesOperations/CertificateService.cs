using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Reporting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.CertificatesOperations.Validations;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common.DTOs;
using MyPoli.Common.Extensions;
using MyPoli.Entities;


namespace MyPoli.BusinessLogic.Implementation.CertificatesOperations
{
    public class CertificateService : BaseService
    {
        private readonly CertificateValidator certificateValidator;
        public CertificateService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            certificateValidator = new CertificateValidator(serviceDependencies);
        }

        public IQueryable<Certificate> IndexList(string sortOrder, string searchString, CurrentUserDto currentUser)
        {
            IQueryable<Certificate> certificates;
            var certificates1 = UnitOfWork.Certificates.Get();
            if (currentUser.Roles.Contains("Student"))
            {
                certificates1 = certificates1.Where(c => c.StudentId == currentUser.Id);
            }
            var certificates2 = certificates1
                .Include(c => c.Student)
                    .ThenInclude(s => s.Group)
                        .ThenInclude(g => g.Specialization)
                .Include(c => c.Student)
                    .ThenInclude(s => s.Person);
           
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                certificates = certificates2.Where(c => c.Student.Person.FirstName.ToUpper().Contains(searchString) || c.Student.Person.LastName.ToUpper().Contains(searchString)
                || c.Student.Group.Name.ToUpper().Contains(searchString) || c.Reason.ToUpper().Contains(searchString) || c.Date.ToString().ToUpper().Contains(searchString));
            } else
            {
                certificates = certificates2;
            }
            return sortOrder switch
            {
                "group_asc" => certificates.OrderBy(c => c.Student.Group.Name),
                "group_desc" => certificates.OrderByDescending(c => c.Student.Group.Name),
                "reason_asc" => certificates.OrderBy(c => c.Reason),
                "reason_desc" => certificates.OrderByDescending(c => c.Reason),
                "date_asc" => certificates.OrderBy(c => c.Date),
                "date_desc" => certificates.OrderByDescending(c => c.Date),
                "name_desc" => certificates.OrderByDescending(c => c.Student.Person.FirstName).ThenByDescending(c => c.Student.Person.LastName),
                _ => certificates.OrderBy(c => c.Student.Person.FirstName).ThenBy(c => c.Student.Person.LastName)
            };
        }

        public List<SelectListItem> GetStudentsOneName()
        {
            var students = UnitOfWork.Students.Get()
                 .Include(s => s.Person)
                 .Select(s => new SelectListItem()
                 {
                     Value = s.Id.ToString(),
                     Text = s.Person.FirstName + " " + s.Person.LastName
                 });
            return students.OrderBy(t => t.Text).ToList();
        }

        public void CreateCertificateFromModel(CertificateVM model)
        {
            ExecuteInTransaction(uow =>
            {
                certificateValidator.Validate(model).ThenThrow(model);
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                var certificate = new Certificate()
                {
                    Id = Guid.NewGuid(),
                    Reason = model.Reason,
                    Date = DateTime.Now,
                    StudentId = model.StudentId
                    
                };

                uow.Certificates.Insert(certificate);
                uow.SaveChanges();

                GenerateCertificate(certificate.Id);
            });

        }

        public byte[] GenerateCertificate(Guid id)
        {
            var completeCertificate = GetCertificateById(id);

            var document = new Document("..\\MyPoli.Common\\template.docx");

            var sender = new Sender()
            {
                Date = completeCertificate.Date.ToString("dd.MM.yyy"),
                Name = completeCertificate.Student.Person.LastName + " " + completeCertificate.Student.Person.FirstName,
                Reason = completeCertificate.Reason,
                Specialization = completeCertificate.Student.Group.Specialization.Description,
                Year = CalculateYearByGroup(completeCertificate.Student.Group.Name)
            };
            var engine = new ReportingEngine();
            engine.BuildReport(document, sender, "sender");
            var outStream = new MemoryStream();
            document.Save(outStream, SaveFormat.Pdf);
            var docBytes = outStream.ToArray();
            return docBytes;
        }

        public async Task<string> GetNameOfFileAsync(Guid id)
        {
            var certificate = await UnitOfWork.Certificates.Get()
                .Include(c => c.Student)
                    .ThenInclude(s => s.Person)
                .Include(c => c.Student)
                    .ThenInclude(s => s.Group)
                        .ThenInclude(g => g.Specialization)
                .FirstOrDefaultAsync(c => c.Id == id);
            var fileName = certificate.Student.Group.Name + "_" + certificate.Student.Person.FirstName + certificate.Student.Person.LastName + ".pdf";
            return fileName;
        }

        private static string CalculateYearByGroup(string group)
        {
            return group[1].ToString();
        }

        public Certificate GetCertificateById(Guid? id)
        {
            var certificate = UnitOfWork.Certificates.Get()
                .Include(c => c.Student)
                    .ThenInclude(s => s.Person)
                .Include(c => c.Student)
                    .ThenInclude(s => s.Group)
                        .ThenInclude(g => g.Specialization)
                .FirstOrDefault(c => c.Id == id);
            return certificate;
        }

        public void DeleteCertificate(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var certificate = uow.Certificates.Get().FirstOrDefault(c => c.Id == id);
                uow.Certificates.Delete(certificate);
                uow.SaveChanges();
            });
        }
    }
}
