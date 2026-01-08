using AutoMapper;
using EduQuiz.Application.UseCases.ClassSlot;

namespace EduQuiz.Application.UseCases
{
    public class GetClassSlotsByTeacherIdUseCaseMappingProfile : Profile
    {
        public GetClassSlotsByTeacherIdUseCaseMappingProfile()
        {
            CreateMap<Domain.Entities.ClassSlot, GetClassSlotsByTeacherIdUseCaseResponse>()
                .ForMember(dest => dest.ClassName,
                    opt => opt.MapFrom(src => src.Class != null ? src.Class.Name : string.Empty))
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src => src.Class != null &&
                        src.Class.Teacher != null &&
                        src.Class.Teacher.Account != null ?
                        $"{src.Class.Teacher.Account.LastName} {src.Class.Teacher.Account.FirstName}" : null));
        }
    }
}
