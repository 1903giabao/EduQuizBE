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

            if (DateTime.TryParse(useCaseInput.Date, out var datetime))
            {
                var date = datetime.Date;
                var nextDate = datetime.Date.AddDays(1);
                classSlotsQuery = classSlotsQuery.Where(s =>
                    s.StartTime >= date &&
                    s.StartTime < nextDate
                );
            }

            var classSlots = await classSlotsQuery.ToListAsync();

            var mappedClassSlots = _mapper.Map<List<GetClassSlotsByStudentIdUseCaseOutput>>(classSlots);

            return mappedClassSlots;
        }
    }
}
