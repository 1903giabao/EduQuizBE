using EduQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<Role> Roles { get; }
        IRepository<Teacher> Teachers { get; }
        IRepository<Student> Students { get; }
        IRepository<Class> Classes { get; }
        IRepository<ClassSlot> ClassSlots { get; }
        IRepository<StudentClass> StudentClasses { get; }
        IRepository<Quiz> Quizzes { get; }
        IRepository<StudentQuiz> StudentQuizzes { get; }
        IRepository<Question> Questions { get; }
        IRepository<QuizQuestion> QuizQuestions { get; }
        IRepository<StudentQuiz> StudentQuizs { get; }
        IRepository<Attendance> Attendances { get; }

        Task<int> SaveChangesAsync();
    }
}
