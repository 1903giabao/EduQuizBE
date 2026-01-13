using Microsoft.AspNetCore.Http;

namespace EduQuiz.Application.UseCases.Account 
{
    public class UpdateAccountAvatarUseCaseInput
    {
        public Guid Id { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
