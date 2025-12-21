
using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Application.UseCases.Student;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeachersUseCase : IUseCase<GetTeachersUseCaseInput, List<GetTeachersUseCaseOutput>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTeachersUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetTeachersUseCaseOutput>> HandleAsync(GetTeachersUseCaseInput useCaseInput)
        {
            var query = _unitOfWork.Teachers.Query()
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

            var teachers = await query
                .OrderBy(x => x.Account.FirstName)
                .Skip((useCaseInput.Page - 1) * useCaseInput.PageSize)
                .Take(useCaseInput.PageSize)
                .ToListAsync();

            var mappedTeachers = _mapper.Map<List<GetTeachersUseCaseOutput>>(teachers);

            return mappedTeachers;
        }
    }
}
