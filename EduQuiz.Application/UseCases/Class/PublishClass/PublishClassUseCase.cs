using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using static EduQuiz.Share.Enums.Enum;

namespace EduQuiz.Application.UseCases.Class
{
    public class PublishClassUseCase : IUseCase<PublishClassUseCaseInput, PublishClassUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublishClassUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PublishClassUseCaseOutput> HandleAsync(PublishClassUseCaseInput useCaseInput)
        {
            var existingClass = await _unitOfWork.Classes.Query()
                    .FirstOrDefaultAsync(x => x.Id == useCaseInput.Id && 
                            x.Status != ClassStatus.REMOVED &&
                            x.Status != ClassStatus.ONGOING);

            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.Id} not found");
            }

            if (existingClass.Status != ClassStatus.PUBLISHED)
            {
                existingClass.Status = ClassStatus.PUBLISHED;
            }
            else
            {
                existingClass.Status = ClassStatus.UNPUBLISHED;
            }

            _unitOfWork.Classes.Update(existingClass);
            await _unitOfWork.SaveChangesAsync();

            return new PublishClassUseCaseOutput();
        }
    }
}