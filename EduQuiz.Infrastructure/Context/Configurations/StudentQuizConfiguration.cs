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
    public class StudentQuizConfiguration : IEntityTypeConfiguration<StudentQuiz>
    {
        public void Configure(EntityTypeBuilder<StudentQuiz> builder)
        {
            builder.ToTable("student_quizzes");
            builder.HasKey(sq => new { sq.StudentId, sq.QuizId });

            builder.Property(a => a.Score)
                .IsRequired();

            builder.Property(x => x.SubmittedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'Asia/Ho_Chi_Minh'");

            builder.Property(a => a.AttemptNumber)
                .IsRequired();

            builder.Property(a => a.IsCompleted)
                .HasDefaultValue(false);

            builder.Property(a => a.Answers)
                .HasColumnType("jsonb");

            builder.Property(a => a.ElapsedTime)
                .HasColumnType("interval")
                .HasDefaultValue(TimeSpan.Zero);

            builder.Property(a => a.IsPaused)
                .HasDefaultValue(false);

            builder.Property(a => a.LastPausedAt)
                .HasColumnType("timestamp without time zone");


            builder.HasOne(sq => sq.Student)
                .WithMany(s => s.StudentQuizzes)
                .HasForeignKey(sq => sq.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sq => sq.Quiz)
                .WithMany(q => q.StudentQuizzes)
                .HasForeignKey(sq => sq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
