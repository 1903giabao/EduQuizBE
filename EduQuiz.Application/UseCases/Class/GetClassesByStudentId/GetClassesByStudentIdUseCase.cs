using AutoMapper;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassesByStudentIdUseCase : IUseCase<GetClassesByStudentIdUseCaseInput, List<GetClassesByStudentIdUseCaseOutput>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassesByStudentIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetClassesByStudentIdUseCaseOutput>> HandleAsync(GetClassesByStudentIdUseCaseInput useCaseInput)
        {
            var student = await _unitOfWork.Students.Query().FirstOrDefaultAsync(t => t.AccountId == useCaseInput.StudentId);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with id: {useCaseInput.StudentId} not found");
            }

            var query = _unitOfWork.Classes.Query()
                .Include(x => x.StudentClasses)
                .ThenInclude(x => x.Student)
                .Where(x => x.StudentClasses.Select(x => x.Student.AccountId).Contains(useCaseInput.StudentId))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(useCaseInput.Keyword))
            {
                var keyword = useCaseInput.Keyword.ToLower();
                query = query.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{keyword}%"));
            }

            var totalItems = await query.CountAsync();

            var classes = await query
                .OrderBy(x => x.Name)
                .Skip((useCaseInput.Page - 1) * useCaseInput.PageSize)
                .Take(useCaseInput.PageSize)
                .ToListAsync();

            var mappedClasses = _mapper.Map<List<GetClassesByStudentIdUseCaseOutput>>(classes);

            return mappedClasses;
        }
    }
}
