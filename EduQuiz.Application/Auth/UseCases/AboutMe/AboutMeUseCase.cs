using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Auth
{
    public class AboutMeUseCase : IUseCase<AboutMeUseCaseInput, AboutMeUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AboutMeUseCase(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AboutMeUseCaseOutput> HandleAsync(AboutMeUseCaseInput useCaseInput)
        {
            var account = await _unitOfWork.Accounts.Query()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(useCaseInput.AccountId));

            if (account == null)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }

            var mappedAccount = _mapper.Map<AboutMeUseCaseOutput>(account);

            return mappedAccount;
        }
    }
}
