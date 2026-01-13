using EduQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Infrastructure.Context.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students");
            builder.HasKey(s => s.Id);

            builder.Property(q => q.ParentPhoneNumber)
                .HasMaxLength(20);

            builder.Property(q => q.Grade)
                .HasMaxLength(10);

            builder.Property(q => q.School)
                .HasMaxLength(100);

            builder.HasOne(s => s.Account)
                .WithOne()
                .HasForeignKey<Student>(s => s.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.StudentClasses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.StudentQuizzes)
                .WithOne(sq => sq.Student)
                .HasForeignKey(sq => sq.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
