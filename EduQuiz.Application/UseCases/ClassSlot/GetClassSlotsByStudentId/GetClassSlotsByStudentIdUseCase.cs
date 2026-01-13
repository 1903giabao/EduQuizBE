using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Domain.Entities;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduQuiz.Application.UseCases.ClassSlot
{
    public class GetClassSlotsByStudentIdUseCase : IUseCase<GetClassSlotsByStudentIdUseCaseInput, GetClassSlotsByStudentIdUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassSlotsByStudentIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetClassSlotsByStudentIdUseCaseOutput> HandleAsync(GetClassSlotsByStudentIdUseCaseInput useCaseInput)
        {
            var classSlotsQuery = _unitOfWork.ClassSlots.Query()
                .Where(s =>
                    s.Class.StudentClasses.Any(sc =>
                        sc.Student.AccountId == useCaseInput.StudentId &&
                        sc.Student.Account.IsActive
                    ) && 
                    s.Class.Status == Share.Enums.Enum.ClassStatus.PUBLISHED
                );

            if (useCaseInput.TeacherId.HasValue)
            {
                classSlotsQuery = classSlotsQuery.Where(s =>
                    s.Class.Teacher != null &&
                    s.Class.Teacher.AccountId == useCaseInput.TeacherId
                );
            }

            if (DateTime.TryParse(useCaseInput.Date, out var datetime))
            {
                var date = datetime.Date;
                var nextDate = datetime.Date.AddDays(1);
                classSlotsQuery = classSlotsQuery.Where(s =>
                    s.StartTime >= date &&
                    s.StartTime < nextDate
                );
            }

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
                .ProjectTo<GetClassSlotsByStudentIdUseCaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GetClassSlotsByStudentIdUseCaseOutput
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
