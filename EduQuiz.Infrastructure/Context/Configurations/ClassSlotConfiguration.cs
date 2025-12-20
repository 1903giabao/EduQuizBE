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
    public class ClassSlotConfiguration : IEntityTypeConfiguration<ClassSlot>
    {
        public void Configure(EntityTypeBuilder<ClassSlot> builder)
        {
            builder.ToTable("class_slots");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(s => s.IsOffline)
                .HasDefaultValue(true);

            builder.Property(s => s.Location)
                .HasMaxLength(100);

            builder.Property(s => s.StartTime)
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            builder.Property(s => s.EndTime)
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(s => s.Class)
                .WithMany(c => c.Slots)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
