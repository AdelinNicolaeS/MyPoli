using MyPoli.BusinessLogic.Implementation.BadWordOperations.Validations;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common.Extensions;
using MyPoli.Entities;
using System;
using System.Linq;

namespace MyPoli.BusinessLogic.Implementation.BadWordOperations
{
    public class BadWordService : BaseService
    {
        private readonly BadWordValidator badWordValidator;
        public BadWordService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            badWordValidator = new BadWordValidator(serviceDependencies);
        }

        public IQueryable<BadWord> IndexToWrite(string v1, string v2)
        {
            var badWords = UnitOfWork.BadWords.Get();
            return badWords;
        }

        public void CreateBadWordFromModel(BadWord model)
        {
            ExecuteInTransaction(uow =>
            {
                badWordValidator.Validate(model).ThenThrow(model);
                model.Id = Guid.NewGuid();
                uow.BadWords.Insert(model);
                uow.SaveChanges();
            });
        }

        public BadWord GetBadWordById(Guid? id)
        {
            return UnitOfWork.BadWords.Get()
                .FirstOrDefault(bw => bw.Id == id);
        }

        public void DeleteBadWord(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var toDelete = UnitOfWork.BadWords.Get()
                    .FirstOrDefault(bw => bw.Id == id);
                uow.BadWords.Delete(toDelete);
                uow.SaveChanges();
            });
        }
    }
}
