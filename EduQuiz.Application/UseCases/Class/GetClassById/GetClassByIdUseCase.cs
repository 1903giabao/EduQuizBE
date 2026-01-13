using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassByIdUseCase : IUseCase<GetClassByIdUseCaseInput, GetClassByIdUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetClassByIdUseCaseOutput> HandleAsync(GetClassByIdUseCaseInput useCaseInput)
        {
            var classExisting = await _unitOfWork.Classes.Query()
                .Where(x => x.Id == useCaseInput.Id)
                .ProjectTo<GetClassByIdUseCaseOutput>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (classExisting == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.Id} not found");
            }

            return classExisting;
        }
    }
}
