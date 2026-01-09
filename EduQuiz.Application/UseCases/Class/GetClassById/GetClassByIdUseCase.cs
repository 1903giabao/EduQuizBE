using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassByIdUseCase : IUseCase<GetClassByIdUseCaseInput, GetClassByIdUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetClassByIdUseCaseOutput> HandleAsync(GetClassByIdUseCaseInput useCaseInput)
        {
            var classExisting = await _unitOfWork.Classes.Query()
                .Include(x => x.Teacher)
                    .ThenInclude(x => x.Account)
                .Include(x => x.StudentClasses)
                    .ThenInclude(x => x.Student)
                        .ThenInclude(x => x.Account)
                .Include(x => x.Slots)
                .Include(x => x.Quizzes)
                .FirstOrDefaultAsync(x => x.Id == useCaseInput.Id);

            var mappedClass = _mapper.Map<GetClassByIdUseCaseOutput>(classExisting);

            return mappedClass;
        }
    }
}
