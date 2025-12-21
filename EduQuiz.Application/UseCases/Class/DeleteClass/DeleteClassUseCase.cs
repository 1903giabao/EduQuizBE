using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class DeleteClassUseCase : IUseCase<DeleteClassUseCaseInput, DeleteClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClassUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteClassUseCaseOutput> HandleAsync(DeleteClassUseCaseInput useCaseInput)
        {
            var existingClass = await _unitOfWork.Classes.Query().FirstOrDefaultAsync(x => x.Id == useCaseInput.Id && x.Status != Share.Enums.Enum.ClassStatus.ONGOING);

            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.Id} not found");
            }

            existingClass.Status = Share.Enums.Enum.ClassStatus.REMOVED;

            _unitOfWork.Classes.Update(existingClass);
            await _unitOfWork.SaveChangesAsync();

            return new DeleteClassUseCaseOutput();
        }
    }
}
