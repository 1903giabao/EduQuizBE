using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class AcademicReportBindingModel
    {
        public string FirstName { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public string Class { get; set; }
        public string Time { get; set; }
        public string EngMonth { get; set; }
        public string VietNamMonth { get; set; }
        public string TestResult { get; set; }
        public string TeacherComment { get; set; }
        public int Writing1 { get; set; }
        public int Writing2 { get; set; }
        public int Writing3 { get; set; }
        public int Reading1 { get; set; }
        public int Reading2 { get; set; }
        public int Reading3 { get; set; }
        public int LexicoGrammar1 { get; set; }
        public int LexicoGrammar2 { get; set; }
        public int LexicoGrammar3 { get; set; }
        public int LearningAttitude1 { get; set; }
        public int LearningAttitude2 { get; set; }
        public int LearningAttitude3 { get; set; }
        public int LearningAttitude4 { get; set; }
    }
}
