using AP.Data.entites;
using AP.Data.entites.helperEntities;
using Microsoft.EntityFrameworkCore;

namespace AP.Data.context;

public class APDbContext : DbContext
{

    public APDbContext(DbContextOptions options) : base(options)
    {
    }

    protected APDbContext()
    {
    }
    #region DbSets    
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<ErrorLogs> ErrorLogs { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);



    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

}
