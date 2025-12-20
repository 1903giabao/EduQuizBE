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
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("quizzes");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(q => q.Class)
                .WithMany(t => t.Quizzes)
                .HasForeignKey(q => q.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(q => q.StudentQuizzes)
                .WithOne(sq => sq.Quiz)
                .HasForeignKey(sq => sq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.QuizQuestions)
                .WithOne(qq => qq.Quiz)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
