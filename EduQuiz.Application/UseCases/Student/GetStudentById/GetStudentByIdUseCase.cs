using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentByIdUseCase : IUseCase<GetStudentByIdUseCaseInput, GetStudentByIdUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStudentByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetStudentByIdUseCaseOutput> HandleAsync(GetStudentByIdUseCaseInput useCaseInput)
        {
            var student = await _unitOfWork.Students.Query()
                .Include(x => x.Account)
                .Include(x => x.StudentClasses)
                    .ThenInclude(x => x.Class)
                        .ThenInclude(x => x.Teacher)
                            .ThenInclude(x => x.Account)
                .FirstOrDefaultAsync(x => x.AccountId == useCaseInput.Id && x.Account.IsActive);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with id: {useCaseInput.Id} not found");
            }

            var mappedStudent = _mapper.Map<GetStudentByIdUseCaseOutput>(student);

            return mappedStudent; 
        }
    }
}
