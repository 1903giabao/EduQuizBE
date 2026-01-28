using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.Services.FileStorageService;
using EduQuiz.Infrastructure.UnitOfWork;
using EduQuiz.Share.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassByIdUseCase : IUseCase<GetClassByIdUseCaseInput, GetClassByIdUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public GetClassByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<GetClassByIdUseCaseOutput> HandleAsync(GetClassByIdUseCaseInput useCaseInput)
        {
            var classExisting = await _unitOfWork.Classes.Query()
                .Where(x => x.Id == useCaseInput.Id)
                .ProjectTo<GetClassByIdUseCaseOutput>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (classExisting == null)
                throw new KeyNotFoundException($"Class with id: {useCaseInput.Id} not found");

            if (classExisting.StudentInClasses != null) 
                await EnrichStudentAvatarsAsync(classExisting.StudentInClasses);

            return classExisting;
        }


        private async Task EnrichStudentAvatarsAsync(
            List<StudentInClass> students)
        {
            if (students == null || students.Count == 0)
                return;

            var tasks = students.Select(async student =>
            {
                if (string.IsNullOrEmpty(student.Avatar))
                    return;

                var fileResult = await _fileStorageService.GetFileAsync(student.Avatar);
                await using var stream = fileResult.Content;
                student.Avatar = await FileExtension.StreamToBase64Async(stream);
            });

            await Task.WhenAll(tasks);
        }
    }
}
