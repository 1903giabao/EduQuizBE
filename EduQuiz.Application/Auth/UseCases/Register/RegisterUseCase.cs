using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Domain.Entities;
using EduQuiz.Infrastructure.Security;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.Auth.UseCases
{
    public class RegisterUseCase : IUseCase<RegisterUseCaseInput, RegisterUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterUseCaseOutput> HandleAsync(RegisterUseCaseInput useCaseInput)
        {
            var existingEmailAccount = await _unitOfWork.Accounts.Query().FirstOrDefaultAsync(x => x.Email == useCaseInput.Email && x.IsActive);
            if (existingEmailAccount != null)
            {
                throw new ArgumentException($"Account with this email already exist");
            }

            var roleName = useCaseInput.IsTeacherRegistration ? "Teacher" : "Student";

            var role = await _unitOfWork.Roles.Query().FirstOrDefaultAsync(x => x.Name.ToLower() == roleName.ToLower());

            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Email = useCaseInput.Email,
                Password = PasswordHasher.Hash(useCaseInput.Password),
                FirstName = useCaseInput.FirstName,
                LastName = useCaseInput.LastName,
                DateOfBirth = useCaseInput.DateOfBirth,
                PhoneNumber = useCaseInput.PhoneNumber,
                Address = useCaseInput.Address,
                Gender = useCaseInput.Gender,
                IsActive = true,
                RoleId = role.Id
            };

            await _unitOfWork.Accounts.AddAsync(newAccount);
            await _unitOfWork.SaveChangesAsync();

            if (useCaseInput.IsTeacherRegistration)
            {
                var newTeacher = new Teacher
                {
                    AccountId = newAccount.Id,
                    Bio = useCaseInput.Bio,
                    Department = useCaseInput.Department,
                };

                await _unitOfWork.Teachers.AddAsync(newTeacher);
            }
            else
            {
                var newStudent = new Student
                {
                    AccountId = newAccount.Id,
                    Grade = useCaseInput.Grade,
                    ParentPhoneNumber = useCaseInput.ParentPhoneNumber,
                    School = useCaseInput.School,
                };

                await _unitOfWork.Students.AddAsync(newStudent);
            }

            await _unitOfWork.SaveChangesAsync();

            return new RegisterUseCaseOutput();
        }
    }
}
