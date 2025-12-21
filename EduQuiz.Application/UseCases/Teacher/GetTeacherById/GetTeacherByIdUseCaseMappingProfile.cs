using AutoMapper;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeacherByIdUseCaseMappingProfile : Profile
    {
        public GetTeacherByIdUseCaseMappingProfile()
        {
            CreateMap<Domain.Entities.Teacher, GetTeacherByIdUseCaseOutput>()
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
                    opt => opt.MapFrom(src => src.Classes));

            CreateMap<Domain.Entities.Class, ClassOfTeacher>();
        }
    }
}
