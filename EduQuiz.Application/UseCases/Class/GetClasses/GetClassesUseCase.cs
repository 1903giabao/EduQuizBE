using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassesUseCase : IUseCase<GetClassesUseCaseInput, GetClassesUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassesUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetClassesUseCaseOutput> HandleAsync(GetClassesUseCaseInput useCaseInput)
        {
            var query = _unitOfWork.Classes.Query();

            if (!string.IsNullOrWhiteSpace(useCaseInput.Keyword))
            {
                var keyword = useCaseInput.Keyword.ToLower();
                query = query.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{keyword}%"));
            }

            if (useCaseInput.TeacherIds != null && useCaseInput.TeacherIds.Any())
            {
                query = query.Where(x => x.Teacher != null && useCaseInput.TeacherIds.Contains(x.Teacher.AccountId));
            }

            if (useCaseInput.StudentIds != null && useCaseInput.StudentIds.Any())
            {
                query = query.Where(x => x.StudentClasses.Any(x => useCaseInput.StudentIds.Contains(x.Student.AccountId)));
            }

            if (useCaseInput.Status.HasValue)
            {
                query = query.Where(x => x.Status == useCaseInput.Status);
            }

            var totalItems = await query.CountAsync();

            var classes = await query
                .OrderBy(x => x.Name)
                .Skip((useCaseInput.Page - 1) * useCaseInput.PageSize)
                .Take(useCaseInput.PageSize)
                .ProjectTo<GetClassesUseCaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GetClassesUseCaseOutput
            {
                Response = classes,
                Meta = new Common.Responses.ApiMeta
                {
                    Page = useCaseInput.Page,
                    PageSize = useCaseInput.PageSize,
                    TotalItems = totalItems
                }
            };
        }
    }
}
