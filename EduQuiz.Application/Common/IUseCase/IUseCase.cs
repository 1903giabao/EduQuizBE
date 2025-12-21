using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Common.IUseCase
{
    public interface IUseCase<TUseCaseInput, TUseCaseOutput>
    {
        Task<TUseCaseOutput> HandleAsync(TUseCaseInput request);
    }
}
