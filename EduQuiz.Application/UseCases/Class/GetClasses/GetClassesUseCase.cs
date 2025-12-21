using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassesUseCase : IUseCase<GetClassesUseCaseInput, List<GetClassesUseCaseOutput>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassesUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetClassesUseCaseOutput>> HandleAsync(GetClassesUseCaseInput useCaseInput)
        {
            var query = _unitOfWork.Classes.Query()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.Account)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(useCaseInput.Keyword))
            {
                var keyword = useCaseInput.Keyword.ToLower();
                query = query.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{keyword}%"));
            }

            var totalItems = await query.CountAsync();

            var classes = await query
                .OrderBy(x => x.Name)
                .Skip((useCaseInput.Page - 1) * useCaseInput.PageSize)
                .Take(useCaseInput.PageSize)
                .ToListAsync();

            var mappedClasses = _mapper.Map<List<GetClassesUseCaseOutput>>(classes);

            return mappedClasses;
        }
    }
}
