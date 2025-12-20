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
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("questions");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Content)
                .IsRequired();

            builder.Property(q => q.Options)
                .HasColumnType("jsonb")
                .IsRequired();

            builder.Property(q => q.Answer)
                .HasColumnType("jsonb")
                .IsRequired();

            builder.Property(q => q.Explanation)
                .HasMaxLength(255);

            builder.Property(q => q.Category)
                .HasMaxLength(50);

            builder.Property(q => q.CreatedAt)
                .HasDefaultValueSql("NOW()");

            builder.HasOne(q => q.Teacher)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(q => q.QuizQuestions)
                .WithOne(qq => qq.Question)
                .HasForeignKey(qq => qq.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
