using FluentValidation;

namespace EduQuiz.Application.UseCases
{
    public class GetClassSlotsByTeacherIdUseCaseValidator : AbstractValidator<GetClassSlotsByTeacherIdUseCaseInput>
    {
        public GetClassSlotsByTeacherIdUseCaseValidator()
        {
            RuleFor(x => x.StartDate)
                .Custom((startDate, context) =>
                {
                    var instance = context.InstanceToValidate;

                    if (startDate != null)
                    {
                        if (!DateTime.TryParse(startDate, out var startDatetime))
                        {
                            context.AddFailure("StartDate", "Start date must be a date");
                        }

                        if (DateTime.TryParse(instance.EndDate, out var endDatetime))
                        {
                            if (startDatetime > endDatetime)
                            {
                                context.AddFailure("StartDate", "Start date must be before end date.");
                            }
                        }
                    }
                });

            RuleFor(x => x.EndDate)
                .Custom((endDate, context) =>
                {
                    if (endDate != null && !DateTime.TryParse(endDate, out var endDatetime))
                    {
                        context.AddFailure("EndDate", "End date must be a date");
                    }
                });
        }
    }
}
