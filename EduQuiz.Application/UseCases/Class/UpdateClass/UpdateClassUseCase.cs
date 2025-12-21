using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Domain.Entities;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class.UpdateClass
{
    public class UpdateClassUseCase : IUseCase<UpdateClassUseCaseInput, UpdateClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClassUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateClassUseCaseOutput> HandleAsync(UpdateClassUseCaseInput useCaseInput)
        {
            var existingClass = await _unitOfWork.Classes.Query().FirstOrDefaultAsync(x => x.Id == useCaseInput.Id && x.Status != Share.Enums.Enum.ClassStatus.REMOVED);

            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.Id} not found");
            }

            if (useCaseInput.TeacherId != null)
            {
                var teacher = await _unitOfWork.Teachers.Query().FirstOrDefaultAsync(t => t.AccountId == useCaseInput.TeacherId);
                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Teacher with id: {useCaseInput.TeacherId} not found");
                }

                existingClass.TeacherId = teacher.Id;
            }
            else
            {
                existingClass.TeacherId = null;
            }

            existingClass.Name = useCaseInput.Name;
            existingClass.Description = useCaseInput.Description;

            _unitOfWork.Classes.Update(existingClass);
            await _unitOfWork.SaveChangesAsync();

            return new UpdateClassUseCaseOutput();
        }
    }
}
