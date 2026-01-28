using AutoMapper;
using EduQuiz.Application.UseCases.Class;

namespace EduQuiz.Application.UseCases.Account
{
    public class GetUserProfileUseCaseMappingProfile : Profile
    {
        public GetUserProfileUseCaseMappingProfile()
        {
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            CreateMap<Domain.Entities.Student, GetUserProfileUseCaseOutput>()
                .ForMember(dest => dest.Avatar,
                    opt => opt.MapFrom(src => src.Account.Avatar))
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.Account.FirstName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.Account.LastName))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.Account.LastName + " " + src.Account.FirstName))
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => src.Account.Role.Name))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(src => src.Account.IsActive))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.Account.CreatedAt))
                .ForMember(dest => dest.EnrolledClassesCount,
                    opt => opt.MapFrom(src => src.StudentClasses.Where(x => x.IsActive).Distinct().Count()))
                .ForMember(dest => dest.CompletedQuizzesCount,
                    opt => opt.MapFrom(src => src.StudentQuizzes.Where(x => x.IsCompleted).Distinct().Count()))
                .ForMember(dest => dest.ToDoQuizzesCount,
                    opt => opt.MapFrom(src => src.StudentQuizzes.Where(x => !x.IsCompleted && now > x.Quiz.StartTime && now < x.Quiz.EndTime).Distinct().Count()));

            CreateMap<Domain.Entities.Teacher, GetUserProfileUseCaseOutput>()
                .ForMember(dest => dest.Avatar,
                    opt => opt.MapFrom(src => src.Account.Avatar))
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.Account.FirstName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.Account.LastName))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.Account.LastName + " " + src.Account.FirstName))
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => src.Account.Role.Name))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(src => src.Account.IsActive))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.Account.CreatedAt))
                .ForMember(dest => dest.TeachingClassesCount,
                    opt => opt.MapFrom(src => src.Classes.Distinct().Count()))
                .ForMember(dest => dest.StudentsCount,
                    opt => opt.MapFrom(src => src.Classes.SelectMany(x => x.StudentClasses).Where(x => x.IsActive).Distinct().Count()))
                .ForMember(dest => dest.PublishedClassesCount,
                    opt => opt.MapFrom(src => src.Classes.Where(x => x.Status == Share.Enums.Enum.ClassStatus.PUBLISHED).Distinct().Count()))
                .ForMember(dest => dest.NonPublishedClassesCount,
                    opt => opt.MapFrom(src => src.Classes.Where(x => x.Status == Share.Enums.Enum.ClassStatus.DRAFT || x.Status == Share.Enums.Enum.ClassStatus.UNPUBLISHED).Distinct().Count()));
        }
    }
}
