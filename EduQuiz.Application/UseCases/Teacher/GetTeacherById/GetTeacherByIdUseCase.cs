using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeacherByIdUseCase : IUseCase<GetTeacherByIdUseCaseInput, GetTeacherByIdUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTeacherByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetTeacherByIdUseCaseOutput> HandleAsync(GetTeacherByIdUseCaseInput useCaseInput)
        {
            var teacher = await _unitOfWork.Teachers.Query()
                .Include(x => x.Account)
                .Include(x => x.Classes)
                .FirstOrDefaultAsync(x => x.AccountId == useCaseInput.Id && x.Account.IsActive);

            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with id: {useCaseInput.Id} not found");
            }

            var mappedTeacher = _mapper.Map<GetTeacherByIdUseCaseOutput>(teacher);

            return mappedTeacher;
        }
    }
}
