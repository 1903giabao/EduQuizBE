using FluentValidation;

namespace EduQuiz.Application.UseCases.Class
{
    public class CreateClassUseCaseValidator : AbstractValidator<CreateClassUseCaseInput>
    {
        public CreateClassUseCaseValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Class name is required")
                .MaximumLength(100).WithMessage("Class name is invalid");

            RuleFor(x => x.StartDate)
                .Custom((startDate, context) =>
                {
                    var instance = context.InstanceToValidate;

                    if (startDate >= instance.EndDate)
                    {
                        context.AddFailure("Start Date", "Start date must be before end date");
                    }

                    if (instance.EndDate - startDate < TimeSpan.FromDays(30))
                    {
                        context.AddFailure("Time Interval", "End date must be 30 or greater days after start date");
                    }
                });

            RuleFor(x => x.SlotInDays)
                .Custom((slots, context) =>
                {
                    var instance = context.InstanceToValidate;

                    foreach (var slot in slots)
                    {
                        if (slot.StartSlotTime >= slot.EndSlotTime)
                        {
                            context.AddFailure("Start Time", "Start time must be before end time");
                        }
                    }
                });
        }
    }
}
