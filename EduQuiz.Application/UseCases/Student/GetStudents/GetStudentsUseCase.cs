using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentsUseCase : IUseCase<GetStudentsUseCaseInput, List<GetStudentsUseCaseOutput>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStudentsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetStudentsUseCaseOutput>> HandleAsync(GetStudentsUseCaseInput useCaseInput)
        {
            var query = _unitOfWork.Students.Query()
                .Include(x => x.Account)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(useCaseInput.Keyword))
            {
                var keyword = useCaseInput.Keyword.ToLower();
                query = query.Where(x =>
                    EF.Functions.Like(x.Account.FirstName.ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(x.Account.LastName.ToLower(), $"%{keyword}%"));
            }

            var totalItems = await query.CountAsync();

            var students = await query
                .OrderBy(x => x.Account.FirstName)
                .Skip((useCaseInput.Page - 1) * useCaseInput.PageSize)
                .Take(useCaseInput.PageSize)
                .ToListAsync();

            var mappedStudents = _mapper.Map<List<GetStudentsUseCaseOutput>>(students);

            return mappedStudents;
        }
    }
}
