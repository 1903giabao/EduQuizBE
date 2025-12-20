using EduQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduQuiz.Infrastructure.Context.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("teachers");
            builder.HasKey(t => t.Id);

            builder.Property(q => q.Bio)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(q => q.Department)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(t => t.Account)
                .WithOne()
                .HasForeignKey<Teacher>(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Classes)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
