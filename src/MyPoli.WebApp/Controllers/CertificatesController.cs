using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPoli.BusinessLogic.Implementation.CertificatesOperations;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common;
using MyPoli.Entities;

using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Student, Secretary")]
    public class CertificatesController : BaseController
    {
        private readonly CertificateService certificateService;

        public CertificatesController(ControllerDependencies dependencies, CertificateService certificateService) : base(dependencies)
        {
            this.certificateService = certificateService;
        }

        // GET: Certificates
        public IActionResult Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GroupSort = sortOrder == "group_asc" ? "group_desc" : "group_asc";
            ViewBag.ReasonSort = sortOrder == "reason_asc" ? "reason_desc" : "reason_asc";
            ViewBag.DateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var certificates = certificateService.IndexList(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Certificate>.Create(certificates, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Certificates/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            var model = new CertificateVM()
            {
                StudentId = CurrentUser.Id,
                StudentIds = new SelectList(certificateService.GetStudentsOneName(), "Value", "Text")
            };
            return View(model);
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CertificateVM certificateVM)
        {
            certificateVM.StudentIds = new SelectList(certificateService.GetStudentsOneName(), "Value", "Text");
            if (ModelState.IsValid)
            {  
                certificateService.CreateCertificateFromModel(certificateVM);
                
                return RedirectToAction(nameof(Index));
            }
            return View(certificateVM);
        }

        // GET: Certificates/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var certificate = certificateService.GetCertificateById(id);
            if (certificate == null)
            {
                return View(Utils.NotFound);
            }
            if(CurrentUser.Roles.Contains("Student") && (certificate.StudentId != CurrentUser.Id))
            {
                return View(Utils.Unauthorized);
            }

            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            certificateService.DeleteCertificate(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadCertificateAsync(Guid id)
        {
            var bytes = certificateService.GenerateCertificate(id);
            var fileDownloadName = await certificateService.GetNameOfFileAsync(id);
            return File(bytes, "application/pdf", fileDownloadName);
        }
    }
}
