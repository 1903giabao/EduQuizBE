using AutoMapper;
using EduQuiz.Application.UseCases.Class;

namespace EduQuiz.Application.UseCases.ClassSlot
{
    public class GetClassSlotsByStudentIdUseCaseMappingProfile : Profile
    {
        public GetClassSlotsByStudentIdUseCaseMappingProfile() 
        {
            CreateMap<Domain.Entities.ClassSlot, GetClassSlotsByStudentIdUseCaseOutput>()
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
