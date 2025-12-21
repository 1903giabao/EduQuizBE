using AutoMapper;

namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentByIdUseCaseMappingProfile : Profile
    {
        public GetStudentByIdUseCaseMappingProfile() 
        {
            CreateMap<Domain.Entities.Student, GetStudentByIdUseCaseOutput>()
                 .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Account.Email))
                 .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.Account.LastName} {src.Account.FirstName}"))
                 .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => src.Account.Gender))
                 .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.Account.DateOfBirth))
                 .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => src.Account.Address))
                 .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.Account.PhoneNumber))
                 .ForMember(dest => dest.Classes,
                    opt => opt.MapFrom(src => src.StudentClasses.Select(sc => sc.Class)));

            CreateMap<Domain.Entities.Class, ClassOfStudent>()
                .ForMember(dest => dest.TeacherId,
                    opt => opt.MapFrom(src => src.Teacher != null ? src.Teacher.AccountId : (Guid?)null))
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher != null && src.Teacher.Account != null ? $"{src.Teacher.Account.LastName} {src.Teacher.Account.FirstName}" : null));
        }
    }
}