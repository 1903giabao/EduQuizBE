using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class AddStudentToClassUseCase : IUseCase<AddStudentToClassUseCaseInput, AddStudentToClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddStudentToClassUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddStudentToClassUseCaseOutput> HandleAsync(AddStudentToClassUseCaseInput useCaseInput)
        {
            var existingClass = await _unitOfWork.Classes.Query()
                    .FirstOrDefaultAsync(x => x.Id == useCaseInput.ClassId &&
                            (x.Status == Share.Enums.Enum.ClassStatus.PUBLISHED ||
                            x.Status == Share.Enums.Enum.ClassStatus.ONGOING));

            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.ClassId} not found");
            }

            var student = await _unitOfWork.Students.Query()
                    .Include(x => x.Account)
                    .Include(x => x.StudentClasses)
                        .ThenInclude(x => x.Class)
                    .FirstOrDefaultAsync(x => x.AccountId == useCaseInput.StudentId && x.Account.IsActive);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with id: {useCaseInput.StudentId} not found");
            }

            var classOfStudent = student.StudentClasses.FirstOrDefault(x => x.ClassId == useCaseInput.ClassId);

            if (classOfStudent != null && classOfStudent.IsActive)
            {
                throw new ArgumentException($"Student is already in class");
            }

            if (classOfStudent == null)
            {
                classOfStudent = new Domain.Entities.StudentClass
                {
                    StudentId = student.Id,
                    ClassId = useCaseInput.ClassId,
                    IsActive = true
                };

                await _unitOfWork.StudentClasses.AddAsync(classOfStudent);
            }
            else
            {
                classOfStudent.IsActive = true;
                _unitOfWork.StudentClasses.Update(classOfStudent);
            }

            await _unitOfWork.SaveChangesAsync();

            return new AddStudentToClassUseCaseOutput();
        }
    }
}