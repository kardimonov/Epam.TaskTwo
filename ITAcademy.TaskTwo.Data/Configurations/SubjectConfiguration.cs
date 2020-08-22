using ITAcademy.TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITAcademy.TaskTwo.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasIndex(s => s.Name)
                .IsUnique();
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}