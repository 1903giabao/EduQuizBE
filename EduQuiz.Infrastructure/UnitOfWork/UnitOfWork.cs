using EduQuiz.Domain.Entities;
using EduQuiz.Infrastructure.Context;

namespace EduQuiz.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EduQuizDbContext _context;

        public IRepository<Account> Accounts { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Teacher> Teachers { get; }
        public IRepository<Student> Students { get; }
        public IRepository<Class> Classes { get; }
        public IRepository<ClassSlot> ClassSlots { get; }
        public IRepository<StudentClass> StudentClasses { get; }
        public IRepository<Quiz> Quizzes { get; }
        public IRepository<StudentQuiz> StudentQuizzes { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<QuizQuestion> QuizQuestions { get; }
        public IRepository<StudentQuiz> StudentQuizs { get; }
        public IRepository<Attendance> Attendances { get; }

        public UnitOfWork(EduQuizDbContext context)
        {
            _context = context;

            Accounts = new Repository<Account>(_context);
            Roles = new Repository<Role>(_context);
            Teachers = new Repository<Teacher>(_context);
            Students = new Repository<Student>(_context);
            Classes = new Repository<Class>(_context);
            ClassSlots = new Repository<ClassSlot>(_context);
            StudentClasses = new Repository<StudentClass>(_context);
            Quizzes = new Repository<Quiz>(_context);
            StudentQuizzes = new Repository<StudentQuiz>(_context);
            Questions = new Repository<Question>(_context);
            QuizQuestions = new Repository<QuizQuestion>(_context);
            StudentQuizs = new Repository<StudentQuiz>(_context);
            Attendances = new Repository<Attendance>(_context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
