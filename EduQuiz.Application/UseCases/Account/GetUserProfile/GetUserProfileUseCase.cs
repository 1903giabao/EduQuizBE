using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.Services.FileStorageService;
using EduQuiz.Infrastructure.UnitOfWork;
using EduQuiz.Share.Enums;
using EduQuiz.Share.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduQuiz.Application.UseCases.Account
{
    public class GetUserProfileUseCase : IUseCase<GetUserProfileUseCaseInput, GetUserProfileUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        public GetUserProfileUseCase(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<GetUserProfileUseCaseOutput> HandleAsync(GetUserProfileUseCaseInput useCaseInput)
        {
            var account = await _unitOfWork.Accounts.Query()
                .Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == useCaseInput.Id);

            if (account == null)
            {
                throw new KeyNotFoundException($"Account with id: {useCaseInput.Id} not found");
            }

            var result = new GetUserProfileUseCaseOutput();

            if (account.Role.Name == RoleConstant.STUDENT)
            {
                var studentQuery = _unitOfWork.Students.Query()
                    .Where(x => x.AccountId == account.Id)
                    .AsQueryable();

                result = await studentQuery
                    .ProjectTo<GetUserProfileUseCaseOutput>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                result.ClassSchedule = await studentQuery
                    .SelectMany(x => x.StudentClasses)
                    .SelectMany(studentClass => studentClass.Class.Slots.Select(slot => new
                    {
                        studentClass.ClassId,
                        studentClass.Class.Name,
                        TeacherName = studentClass.Class.Teacher != null ? studentClass.Class.Teacher.Account.FirstName + " " + studentClass.Class.Teacher.Account.LastName : string.Empty,
                        slot.Location,
                        Slot = slot
                    }))
                    .GroupBy(x => new { x.ClassId, x.Name, x.TeacherName, x.Location })
                    .Select(x => new ClassSchedule
                    {
                        ClassId = x.Key.ClassId.ToString(),
                        ClassName = x.Key.Name,
                        Location = x.Select(x => x.Location).Distinct().ToList(),
                        TeacherName = x.Key.TeacherName,
                        Times = DayExtension.ToListDayFormat(x.Select(x => x.Slot.StartTime).ToList())
                    })
                    .ToListAsync();
            }
            else if (account.Role.Name == RoleConstant.TEACHER)
            {
                var teacherQuery = _unitOfWork.Teachers.Query()
                    .Where(x => x.AccountId == account.Id)
                    .AsQueryable();

                result = await teacherQuery
                    .ProjectTo<GetUserProfileUseCaseOutput>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                result.ClassSchedule = await teacherQuery
                    .SelectMany(t => t.Classes)
                    .SelectMany(cl => cl.Slots.Select(slot => new
                    {
                        cl.Id,
                        cl.Name,
                        TeacherName = cl.Teacher != null ? cl.Teacher.Account.FirstName + " " + cl.Teacher.Account.LastName : string.Empty,
                        slot.Location,
                        Slot = slot
                    }))
                    .GroupBy(x => new { x.Id, x.Name, x.TeacherName })
                    .Select(x => new ClassSchedule
                    {
                        ClassId = x.Key.Id.ToString(),
                        ClassName = x.Key.Name,
                        Location = x.Select(x => x.Location).Distinct().ToList(),
                        TeacherName = x.Key.TeacherName,
                        Times = DayExtension.ToListDayFormat(x.Select(x => x.Slot.StartTime).ToList())
                    })
                    .ToListAsync();
            }

            if (result.Avatar != null)
            {
                var fileResult = await _fileStorageService.GetFileAsync(result.Avatar);
                await using var stream = fileResult.Content;
                result.Avatar = await FileExtension.StreamToBase64Async(stream);
            }

            return result;
        }
    }
}
