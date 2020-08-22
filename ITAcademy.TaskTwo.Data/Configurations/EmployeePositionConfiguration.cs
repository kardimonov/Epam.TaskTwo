using ITAcademy.TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITAcademy.TaskTwo.Data.Configurations
{
    public class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
    {
        public void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {
            builder.HasKey(ep => new { ep.EmployeeId, ep.PositionId });

            builder.HasOne(ep => ep.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(ep => ep.EmployeeId);

            builder.HasOne(ep => ep.Position)
                .WithMany(p => p.Appointments)
                .HasForeignKey(ep => ep.PositionId);
        }
    }
}