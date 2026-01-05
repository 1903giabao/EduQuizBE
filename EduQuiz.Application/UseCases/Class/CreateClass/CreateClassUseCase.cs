using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Domain.Entities;
using EduQuiz.Infrastructure.UnitOfWork;
using EduQuiz.Share.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class CreateClassUseCase : IUseCase<CreateClassUseCaseInput, CreateClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClassUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateClassUseCaseOutput> HandleAsync(CreateClassUseCaseInput useCaseInput)
        {
            if (useCaseInput.TeacherId != null)
            {
                var teacher = await _unitOfWork.Teachers.Query().FirstOrDefaultAsync(t => t.AccountId == useCaseInput.TeacherId);

                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Teacher with id: {useCaseInput.TeacherId} not found");
                }
            }

            var newClass = new Domain.Entities.Class
            {
                Name = useCaseInput.Name,
                Description = useCaseInput.Description,
                TeacherId = useCaseInput.TeacherId,
                Status = Share.Enums.Enum.ClassStatus.DRAFT,
            };

            await _unitOfWork.Classes.AddAsync(newClass);

            var newSlots = new List<Domain.Entities.ClassSlot>();
            foreach (var slotTime in useCaseInput.SlotInDays)
            {
                var datesInTimeRange = DayExtension.ExtractDateByDayAndTimeRange(useCaseInput.StartDate, useCaseInput.EndDate, slotTime.Day);

                foreach (var date in datesInTimeRange)
                {
                    var startTime = new DateTime(date: date, time: slotTime.StartSlotTime);
                    var endTime = new DateTime(date: date, time: slotTime.EndSlotTime);
                    var newSlot = new Domain.Entities.ClassSlot
                    {
                        ClassId = newClass.Id,
                        StartTime = startTime,
                        EndTime = endTime,
                        Location = slotTime.Location,
                        Description = string.Empty
                    };

                    newSlots.Add(newSlot);
                }
            }

            await _unitOfWork.ClassSlots.AddRangeAsync(newSlots);

            await _unitOfWork.SaveChangesAsync();

            return new CreateClassUseCaseOutput();
        }
    }
}
