using EduQuiz.Application.UseCases.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Services
{
    public interface ITemplateService
    {
        List<AcademicReportBindingModel> BindAcademicReportModel(string code, List<StudentReportModel> inputModel);
    }
}
