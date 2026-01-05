using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class AddTeacerToClassUseCase : IUseCase<AddTeacerToClassUseCaseInput, AddTeacerToClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTeacerToClassUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddTeacerToClassUseCaseOutput> HandleAsync(AddTeacerToClassUseCaseInput useCaseInput)
        {
            var existingClass = await _unitOfWork.Classes.Query()
                    .FirstOrDefaultAsync(x => x.Id == useCaseInput.ClassId &&
                            x.Status != Share.Enums.Enum.ClassStatus.REMOVED);

            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.ClassId} not found");
            }

            var teacher = await _unitOfWork.Teachers.Query()
                    .Include(x => x.Account)
                    .FirstOrDefaultAsync(x => x.AccountId == useCaseInput.TeacherId && x.Account.IsActive);

            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with id: {useCaseInput.TeacherId} not found");
            }

            var classOfTeacher = teacher.Classes.FirstOrDefault(x => x.Id == useCaseInput.ClassId);

            if (classOfTeacher != null && classOfTeacher.Status != Share.Enums.Enum.ClassStatus.REMOVED)
            {
                throw new ArgumentException($"Teacher is already in class");
            }

            if (classOfTeacher == null)
            {
                existingClass.TeacherId = teacher.Id;
                await _unitOfWork.Classes.AddAsync(existingClass);
            }

            await _unitOfWork.SaveChangesAsync();

            return new AddTeacerToClassUseCaseOutput();
        }
    }
}
