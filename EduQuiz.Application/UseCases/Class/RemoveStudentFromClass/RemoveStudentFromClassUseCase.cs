using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Application.UseCases.Student;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class RemoveStudentFromClassUseCase : IUseCase<RemoveStudentFromClassUseCaseInput, RemoveStudentFromClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RemoveStudentFromClassUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RemoveStudentFromClassUseCaseOutput> HandleAsync(RemoveStudentFromClassUseCaseInput useCaseInput)
        {
            var existingClass = await _unitOfWork.Classes.Query()
                    .FirstOrDefaultAsync(x => x.Id == useCaseInput.ClassId &&
                            (x.Status == Share.Enums.Enum.ClassStatus.PUBLISHED ||
                            x.Status == Share.Enums.Enum.ClassStatus.ONGOING));

            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.ClassId} not found");
            }

            var studentInClass = await _unitOfWork.StudentClasses.Query()
                    .Include(x => x.Student)
                    .FirstOrDefaultAsync(x => x.ClassId == useCaseInput.ClassId &&
                            x.Student.AccountId == useCaseInput.StudentId && x.IsActive);

            if (studentInClass == null)
            {
                throw new ArgumentException($"Student is not in thic class");
            }

            studentInClass.IsActive = false;

            _unitOfWork.StudentClasses.Update(studentInClass);
            await _unitOfWork.SaveChangesAsync();

            return new RemoveStudentFromClassUseCaseOutput();
        }
    }
}