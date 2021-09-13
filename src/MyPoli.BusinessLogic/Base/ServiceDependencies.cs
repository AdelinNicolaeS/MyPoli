using AutoMapper;
using MyPoli.Common.DTOs;
using MyPoli.DataAccess;

namespace MyPoli.BusinessLogic
{
    public class ServiceDependencies
    {
        public IMapper Mapper { get; set; }
        public UnitOfWork UnitOfWork { get; set; }
        public CurrentUserDto CurrentUser { get; set; }

        public ServiceDependencies(IMapper mapper, UnitOfWork unitOfWork, CurrentUserDto currentUser)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            CurrentUser = currentUser;
        }
    }
}
