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
    public class GetClassesByTeacherIdUseCase : IUseCase<GetClassesByTeacherIdUseCaseInput, List<GetClassesByTeacherIdUseCaseOutput>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassesByTeacherIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetClassesByTeacherIdUseCaseOutput>> HandleAsync(GetClassesByTeacherIdUseCaseInput useCaseInput)
        {
            var teacher = await _unitOfWork.Teachers.Query().FirstOrDefaultAsync(t => t.AccountId == useCaseInput.TeacherId);

            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with id: {useCaseInput.TeacherId} not found");
            }

            var query = _unitOfWork.Classes.Query()
                .Include(x => x.Teacher)
                .Where(x => x.Teacher != null && x.Teacher.AccountId == useCaseInput.TeacherId)
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

            var mappedClasses = _mapper.Map<List<GetClassesByTeacherIdUseCaseOutput>>(classes);

            return mappedClasses;
        }
    }
}
