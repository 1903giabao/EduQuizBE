using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class.DeleteClass
{
    public class DeleteClassUseCaseValidator : AbstractValidator<DeleteClassUseCaseInput>
    {
        public DeleteClassUseCaseValidator() { }
    }
}
