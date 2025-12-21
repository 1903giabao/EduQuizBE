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
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher == null ? null : src.Teacher.Account.LastName + " " + src.Teacher.Account.FirstName));
        }
    }
}
