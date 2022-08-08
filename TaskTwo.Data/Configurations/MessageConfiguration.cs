using TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskTwo.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.Addressee)
                .WithMany(e => e.Messages)
                .HasForeignKey(m => m.AddresseeId);

            builder.Property(m => m.AddresseeId)
                .IsRequired();
            builder.Property(m => m.Content)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(m => m.Type)
                .IsRequired();
            builder.Property(m => m.TimeCreated)
                .IsRequired()
                .HasColumnType("datetime2(0)");
            builder.Property(m => m.DispatchResult)
                .IsRequired();
        }
    }
}