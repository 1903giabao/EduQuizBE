using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class.DeleteClass
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
            var existingClass = _unitOfWork.Classes.Query().FirstOrDefaultAsync(x => x.Id == useCaseInput.Id);

            return new DeleteClassUseCaseOutput();
        }
    }
}
