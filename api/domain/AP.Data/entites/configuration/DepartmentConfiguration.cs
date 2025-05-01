using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AP.Data.entites.configuration;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Departments>
{
    public void Configure(EntityTypeBuilder<Departments> builder)
    {
        builder.ToTable("Departments");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Description).HasMaxLength(100);

    }
}
