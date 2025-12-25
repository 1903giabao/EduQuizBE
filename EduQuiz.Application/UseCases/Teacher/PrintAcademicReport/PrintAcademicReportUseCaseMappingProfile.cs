using AutoMapper;
using EduQuiz.Share.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Teacher.PrintAcademicReport
{
    public class PrintAcademicReportUseCaseMappingProfile : Profile
    {
        public PrintAcademicReportUseCaseMappingProfile()
        {
            CreateMap<StudentReportModel, AcademicReportBindingModel>()
                 .ForMember(dest => dest.StudentName,
                    opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName}".ToUpper()))
                 .ForMember(dest => dest.EngMonth,
                    opt => opt.MapFrom(src => src.ReportMonth))
                 .ForMember(dest => dest.VietNamMonth,
                    opt => opt.MapFrom(src => src.ReportMonth.ToVietnameseMonth()))
                 .ForMember(dest => dest.Writing1,
                    opt => opt.MapFrom(src => src.LearningArea.Writing[0]))
                 .ForMember(dest => dest.Writing2,
                    opt => opt.MapFrom(src => src.LearningArea.Writing[1]))
                 .ForMember(dest => dest.Writing3,
                    opt => opt.MapFrom(src => src.LearningArea.Writing[2]))
                 .ForMember(dest => dest.Reading1,
                    opt => opt.MapFrom(src => src.LearningArea.Reading[0]))
                 .ForMember(dest => dest.Reading2,
                    opt => opt.MapFrom(src => src.LearningArea.Reading[1]))
                 .ForMember(dest => dest.Reading3,
                    opt => opt.MapFrom(src => src.LearningArea.Reading[2]))
                 .ForMember(dest => dest.LexicoGrammar1,
                    opt => opt.MapFrom(src => src.LearningArea.LexicoGrammar[0]))
                 .ForMember(dest => dest.LexicoGrammar2,
                    opt => opt.MapFrom(src => src.LearningArea.LexicoGrammar[1]))
                 .ForMember(dest => dest.LexicoGrammar3,
                    opt => opt.MapFrom(src => src.LearningArea.LexicoGrammar[2]))
                 .ForMember(dest => dest.LearningAttitude1,
                    opt => opt.MapFrom(src => src.LearningArea.LearningAttitude[0]))
                 .ForMember(dest => dest.LearningAttitude2,
                    opt => opt.MapFrom(src => src.LearningArea.LearningAttitude[1]))
                 .ForMember(dest => dest.LearningAttitude3,
                    opt => opt.MapFrom(src => src.LearningArea.LearningAttitude[2]))
                 .ForMember(dest => dest.LearningAttitude4,
                    opt => opt.MapFrom(src => src.LearningArea.LearningAttitude[3]));
        }
    }
}
