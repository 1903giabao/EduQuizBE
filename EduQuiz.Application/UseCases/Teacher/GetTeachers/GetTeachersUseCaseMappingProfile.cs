using AutoMapper;
using EduQuiz.Application.UseCases.Student;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeachersUseCaseMappingProfile : Profile
    {
        public GetTeachersUseCaseMappingProfile()
        {
            CreateMap<Domain.Entities.Teacher, GetTeachersUseCaseOutput>()
                 .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Account.Id))
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
                    opt => opt.MapFrom(src => src.Account.PhoneNumber));
        }
    }
}
