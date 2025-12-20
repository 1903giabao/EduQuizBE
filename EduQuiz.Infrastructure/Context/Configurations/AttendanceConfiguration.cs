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
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("attendances");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.IsAttended)
                .HasDefaultValue(false);

            builder.Property(a => a.MarkedAt)
                .HasDefaultValueSql("NOW()");

            builder.Property(a => a.Notes)
                .HasMaxLength(500);

            builder.HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.ClassSlot)
                .WithMany(cs => cs.Attendances)
                .HasForeignKey(a => a.ClassSlotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
