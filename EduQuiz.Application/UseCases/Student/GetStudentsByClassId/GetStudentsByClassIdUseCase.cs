
using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentsByClassIdUseCase : IUseCase<GetStudentsByClassIdUseCaseInput, List<GetStudentsByClassIdUseCaseOutput>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStudentsByClassIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetStudentsByClassIdUseCaseOutput>> HandleAsync(GetStudentsByClassIdUseCaseInput useCaseInput)
        {
            var existingClass = await _unitOfWork.Classes.Query().FirstOrDefaultAsync(x => x.Id == useCaseInput.ClassId);

            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Class with id: {useCaseInput.ClassId} not found");
            }

            var query = _unitOfWork.Students.Query()
                .Include(x => x.Account)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(useCaseInput.Keyword))
            {
                var keyword = useCaseInput.Keyword.ToLower();
                query = query.Where(x => 
                    EF.Functions.Like(x.Account.FirstName.ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(x.Account.LastName.ToLower(), $"%{keyword}%"));
            }

            var totalItems = await query.CountAsync();

            var students = await query
                .OrderBy(x => x.Account.FirstName)
                .Skip((useCaseInput.Page - 1) * useCaseInput.PageSize)
                .Take(useCaseInput.PageSize)
                .ToListAsync();

            var mappedStudents = _mapper.Map<List<GetStudentsByClassIdUseCaseOutput>>(students);

            return mappedStudents;
        }
    }
}
