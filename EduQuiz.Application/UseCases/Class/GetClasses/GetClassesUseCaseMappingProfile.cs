using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassesUseCaseMappingProfile : Profile
    {
        public GetClassesUseCaseMappingProfile()
        {
            CreateMap<Domain.Entities.Class, GetClassesUseCaseOutput>()
                .ForMember(dest => dest.TeacherId,
                    opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.AccountId : (Guid?)null))
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher != null && src.Teacher.Account != null  ? $"{src.Teacher.Account.LastName} {src.Teacher.Account.FirstName}" : null));
        }
    }
}
