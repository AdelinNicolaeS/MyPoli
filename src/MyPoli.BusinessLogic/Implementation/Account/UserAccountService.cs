using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.Account.Validations;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common;
using MyPoli.Common.DTOs;
using MyPoli.Common.Extensions;
using MyPoli.DataAccess;
using MyPoli.Entities;
using MyPoli.Entities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPoli.BusinessLogic.Implementation.Account
{
    public class UserAccountService : BaseService
    {
        private readonly RegisterUserValidator RegisterUserValidator;
        private readonly ChangePasswordValidator changePasswordValidator;

        public UserAccountService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.RegisterUserValidator = new RegisterUserValidator();
            this.changePasswordValidator = new ChangePasswordValidator(dependencies);
        }

        public CurrentUserDto Login(string email, string password)
        {
            var passwordHash = Utils.MyHashFunction(password);

            var person = UnitOfWork.People.Get()
                .Include(p => p.PersonRoles)
                    .ThenInclude(pr => pr.Role)
                .FirstOrDefault(p => p.Email == email && p.PasswordHash == passwordHash);

            if (person == null)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            person.LastLogin = DateTime.Now;
            UnitOfWork.People.Update(person);
            UnitOfWork.SaveChanges();

            var curr = new CurrentUserDto
            {
                Id = person.Id,
                Email = person.Email,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsAuthenticated = true,
                Roles = person.PersonRoles.Select(ur => ur.Role.Name).ToList()
            };
            return curr;
        }

        public void RegisterNewUser(RegisterModel model)
        {
            ExecuteInTransaction(uow =>
            {
                RegisterUserValidator.Validate(model).ThenThrow(model);

                var user = Mapper.Map<RegisterModel, User>(model);

                //user.PasswordHash == 

                user.PersonRoles = new List<PersonRole>
                {
                    new PersonRole { RoleId = (int)RoleTypes.User }
                };

                uow.Users.Insert(user);
                

                uow.SaveChanges();
            });
        }

        public List<ListItemModel<string, Guid>> GetPeople()
        {
            return UnitOfWork.People.Get()
                .Select(u => new ListItemModel<string, Guid>
                {
                    Text = $"{u.FirstName} {u.LastName}",
                    Value = u.Id
                })
                .ToList();
        }

        public void DisableUser() { }

        public void ChangePassword(ChangePasswordModel changePasswordModel) {
            ExecuteInTransaction(uow => {
                changePasswordValidator.Validate(changePasswordModel).ThenThrow(changePasswordModel);

                var oldPassHash = Utils.MyHashFunction(changePasswordModel.OldPassword);
                var person = uow.People.Get().FirstOrDefault(p => p.Email == changePasswordModel.Email && p.PasswordHash == oldPassHash);
                if(person != null)
                {
                    person.PasswordHash = Utils.MyHashFunction(changePasswordModel.NewPassword);
                }
                uow.People.Update(person);
                uow.SaveChanges();
            });
        }
    }
}
