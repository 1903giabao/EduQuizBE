using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Domain.Entities;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduQuiz.Application.UseCases.ClassSlot
{
    public class GetClassSlotsByStudentIdUseCase : IUseCase<GetClassSlotsByStudentIdUseCaseInput, List<GetClassSlotsByStudentIdUseCaseOutput>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassSlotsByStudentIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetClassSlotsByStudentIdUseCaseOutput>> HandleAsync(GetClassSlotsByStudentIdUseCaseInput useCaseInput)
        {
            var classSlotsQuery = _unitOfWork.ClassSlots.Query()
                .Include(x => x.Class)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.Account)
                .Where(s =>
                    s.Class.StudentClasses.Any(sc =>
                        sc.Student.AccountId == useCaseInput.StudentId &&
                        sc.Student.Account.IsActive
                    )
                );

            if (useCaseInput.TeacherId.HasValue)
            {
                classSlotsQuery = classSlotsQuery.Where(s =>
                    s.Class.Teacher != null &&
                    s.Class.Teacher.AccountId == useCaseInput.TeacherId
                );
            }

            var date = useCaseInput.Date?.Date;
            var nextDate = date?.AddDays(1);

            if (date.HasValue)
            {
                classSlotsQuery = classSlotsQuery.Where(s =>
                    s.StartTime >= date.Value &&
                    s.StartTime < nextDate!.Value
                );
            }

            var classSlots = await classSlotsQuery.ToListAsync();

            var mappedClassSlots = _mapper.Map<List<GetClassSlotsByStudentIdUseCaseOutput>>(classSlots);

            return mappedClassSlots;
        }
    }
}
