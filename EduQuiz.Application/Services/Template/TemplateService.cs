using AutoMapper;
using EduQuiz.Application.UseCases.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EduQuiz.Share.Enums.Enum;

namespace EduQuiz.Application.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IMapper _mapper;
        public TemplateService(IMapper mapper) 
        { 
            _mapper = mapper;
        }

        public List<AcademicReportBindingModel> BindAcademicReportModel(string code, List<StudentReportModel> inputModel)
        {
            if (!Enum.TryParse<GroupCodeEnum>(code, true, out var groupCode) && groupCode != GroupCodeEnum.ACADEMIC_REPORT)
            {
                throw new ArgumentException("Unsupported group code");
            }

            var result = _mapper.Map<List<AcademicReportBindingModel>>(inputModel);
            return result;
        }
    }
}
