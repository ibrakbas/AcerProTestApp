using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AP.Data.entites.configuration;

public sealed class WorkerConfiguration : IEntityTypeConfiguration<Workers>
{
    public void Configure(EntityTypeBuilder<Workers> builder)
    {
        builder.ToTable("Workers");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).HasMaxLength(100);
        builder.HasOne(b => b.Department).WithMany(m => m.Workers).HasForeignKey(m => m.DepartmentId);

    }
}
