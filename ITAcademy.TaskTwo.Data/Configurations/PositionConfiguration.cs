using ITAcademy.TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITAcademy.TaskTwo.Data.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasIndex(p => p.Name)
                .IsUnique();
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.MaxNumber)
                .IsRequired();
        }
    }
}
