using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class
{
    public class CreateClassUseCase : IUseCase<CreateClassUseCaseInput, CreateClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClassUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateClassUseCaseOutput> HandleAsync(CreateClassUseCaseInput useCaseInput)
        {
            if (useCaseInput.TeacherId != null)
            {
                var teacher = await _unitOfWork.Teachers.Query().FirstOrDefaultAsync(t => t.AccountId == useCaseInput.TeacherId);

                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Teacher with id {useCaseInput.TeacherId} not found");
                }
            }

            var newClass = new Domain.Entities.Class
            {
                Name = useCaseInput.Name,
                Description = useCaseInput.Description,
                TeacherId = useCaseInput.TeacherId
            };

            await _unitOfWork.Classes.AddAsync(newClass);
            await _unitOfWork.SaveChangesAsync();

            return new CreateClassUseCaseOutput();
        }
    }
}
