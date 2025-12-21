using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class.UpdateClass
{
    public class UpdateClassUseCaseValidator : AbstractValidator<UpdateClassUseCaseInput>
    {
        public UpdateClassUseCaseValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Class name is required")
                .MaximumLength(100).WithMessage("Class name is invalid");
        }
    }
}
