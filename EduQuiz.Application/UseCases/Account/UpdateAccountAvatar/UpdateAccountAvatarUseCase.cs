using AutoMapper;
using DocumentFormat.OpenXml.VariantTypes;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.Services.FileStorageService;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Account
{
    public class UpdateAccountAvatarUseCase : IUseCase<UpdateAccountAvatarUseCaseInput, UpdateAccountAvatarUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _storage;
        public UpdateAccountAvatarUseCase(IUnitOfWork unitOfWork, IFileStorageService storage)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
        }

        public async Task<UpdateAccountAvatarUseCaseOutput> HandleAsync(UpdateAccountAvatarUseCaseInput useCaseInput)
        {
            var account = await _unitOfWork.Accounts.Query()
                .Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == useCaseInput.Id);

            if (account == null)
            {
                throw new KeyNotFoundException($"Account with id: {useCaseInput.Id} not found");
            }

            var file = useCaseInput.Avatar;
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required");

            await using var stream = file.OpenReadStream();

            await _storage.UploadAsync(stream, file.FileName, file.ContentType);

            return new UpdateAccountAvatarUseCaseOutput();
        }
    }
}
