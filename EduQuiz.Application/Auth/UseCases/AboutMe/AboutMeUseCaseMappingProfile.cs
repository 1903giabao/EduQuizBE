using AutoMapper;
using EduQuiz.Application.UseCases.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Auth
{
    public class AboutMeUseCaseMappingProfile : Profile
    {
        public AboutMeUseCaseMappingProfile() 
        {
            CreateMap<Domain.Entities.Account, AboutMeUseCaseOutput>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null))
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName}"));
        }
    }
}
