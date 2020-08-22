using ITAcademy.TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITAcademy.TaskTwo.Data.Configurations
{
    public class EmployeeSubjectConfiguration : IEntityTypeConfiguration<EmployeeSubject>
    {
        public void Configure(EntityTypeBuilder<EmployeeSubject> builder)
        {
            builder.HasKey(t => new { t.EmployeeId, t.SubjectId });

            builder.HasOne(es => es.Employee)
                .WithMany(e => e.Assignments)
                .HasForeignKey(sc => sc.EmployeeId);

            builder.HasOne(es => es.Subject)
                .WithMany(s => s.Assignments)
                .HasForeignKey(es => es.SubjectId);
        }
    }
}