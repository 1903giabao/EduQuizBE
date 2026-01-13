using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Application.UseCases.ClassSlot;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases
{
    public class GetClassSlotsByTeacherIdUseCase : IUseCase<GetClassSlotsByTeacherIdUseCaseInput, GetClassSlotsByTeacherIdUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassSlotsByTeacherIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetClassSlotsByTeacherIdUseCaseOutput> HandleAsync(GetClassSlotsByTeacherIdUseCaseInput useCaseInput)
        {
            var classSlotsQuery = _unitOfWork.ClassSlots.Query()
                .Where(s => 
                    s.Class.Teacher != null &&
                    s.Class.Teacher.AccountId == useCaseInput.TeacherId &&
                    s.Class.Status == Share.Enums.Enum.ClassStatus.PUBLISHED
                );

            if (DateTime.TryParse(useCaseInput.StartDate, out var startDatetime) &&
                DateTime.TryParse(useCaseInput.EndDate, out var endDatetime))
            {
                var date = startDatetime.Date;
                var endDate = endDatetime.Date.AddDays(1);
                classSlotsQuery = classSlotsQuery.Where(s =>
                    s.StartTime >= date &&
                    s.StartTime < endDate
                );
            }

            var totalItems = await classSlotsQuery.CountAsync();

            var classSlots = await classSlotsQuery
                .OrderBy(x => x.StartTime)
                .Skip((useCaseInput.Page - 1) * useCaseInput.PageSize)
                .Take(useCaseInput.PageSize)
                .ProjectTo<GetClassSlotsByTeacherIdUseCaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GetClassSlotsByTeacherIdUseCaseOutput
            {
                Response = classSlots.OrderBy(x => x.StartTime).ToList(),
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
