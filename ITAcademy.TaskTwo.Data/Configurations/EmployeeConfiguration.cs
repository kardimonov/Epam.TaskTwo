using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITAcademy.TaskTwo.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.PrimaryPhone)
                .WithOne()
                .HasForeignKey<Employee>(e => e.PrimaryPhoneId);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.SecondName)
                .HasMaxLength(50);
            builder.Property(e => e.SurName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Email)
                .HasMaxLength(50);
            builder.Property(e => e.Birth)
                .IsRequired()
                .HasColumnType("date");
            builder.Property(e => e.Communication)
                .IsRequired()
                .HasDefaultValue(MessageType.Email);
        }
    }
}