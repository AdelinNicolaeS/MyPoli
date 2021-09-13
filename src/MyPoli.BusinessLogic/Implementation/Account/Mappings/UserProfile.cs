using AutoMapper;
using MyPoli.Entities;
using System;

namespace MyPoli.BusinessLogic.Implementation.Account
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(a => a.Id, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.PasswordHash, a => a.MapFrom(s => s.Password));
        }
    }
}
