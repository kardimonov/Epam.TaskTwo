using TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskTwo.Data.Configurations
{
    public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasOne(p => p.Employee)
                .WithMany(e => e.Phones)
                .HasForeignKey(p => p.EmployeeId);

            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(13);
        }
    }
}