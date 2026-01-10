using AutoMapper;
using EduQuiz.Share.Extensions;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassByIdUseCaseMappingProfile : Profile
    {
        public GetClassByIdUseCaseMappingProfile()
        {
            CreateMap<Domain.Entities.Class, GetClassByIdUseCaseOutput>()
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher == null ? null : src.Teacher.Account.LastName + " " + src.Teacher.Account.FirstName))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Locations,
                    opt => opt.MapFrom(src => src.Slots.Select(x => x.Location).Distinct()))
                .ForMember(dest => dest.Schedules,
                    opt => opt.MapFrom(src => src.Slots.Select(x => DayExtension.ToDayOfWeek3Letters(x.StartTime)).Distinct().ToList()))
                .ForMember(dest => dest.NumOfStudents,
                    opt => opt.MapFrom(src => src.StudentClasses.Distinct().Count()))
                .ForMember(dest => dest.StudentInClasses,
                    opt => opt.MapFrom(src => src.StudentClasses.Select(x => x.Student)))
                .ForMember(dest => dest.QuizOfClasses,
                    opt => opt.MapFrom(src => src.Quizzes))
                .ForMember(dest => dest.ScheduleOfClasses,
                    opt => opt.MapFrom(src => src.Slots.OrderBy(x => x.StartTime)));

            CreateMap<Domain.Entities.Student, StudentInClass>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.Account.FirstName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.Account.LastName))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.Account.LastName} {src.Account.FirstName}"));

            CreateMap<Domain.Entities.Quiz, QuizOfClass>();

            CreateMap<Domain.Entities.ClassSlot, ScheduleOfClass>();

        }
    }
}
