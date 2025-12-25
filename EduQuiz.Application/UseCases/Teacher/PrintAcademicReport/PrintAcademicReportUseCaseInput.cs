namespace EduQuiz.Application.UseCases.Teacher
{
    public class PrintAcademicReportUseCaseInput
    {
        public List<StudentReportModel> StudentReports { get; set; }

    }

    public class StudentReportModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TeacherName { get; set; }
        public string Class { get; set; }
        public string Time { get; set; }
        public string ReportMonth { get; set; }
        public string TestResult { get; set; }
        public string TeacherComment { get; set; }
        public LearningArea LearningArea { get; set; }
    }

    public class LearningArea
    {
        public List<int> Writing {  get; set; }
        public List<int> Reading {  get; set; }
        public List<int> LexicoGrammar {  get; set; }
        public List<int> LearningAttitude {  get; set; }
    }
}
