using AP.Data.entites;
using AP.Data.entites.helperEntities;
using Microsoft.EntityFrameworkCore;

namespace AP.Data.context;

/// <summary> 
///  Veritabanı context sınıfı
/// </summary>
 
public class APDbContext : DbContext
{

    public APDbContext(DbContextOptions options) : base(options)
    {
    }

    protected APDbContext()
    {
    }

    /// <summary> 
    ///  Tabloların DbSet'leri, veritabanı tablolarını temsil eder
    /// </summary>
    #region DbSets    
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<ErrorLogs> ErrorLogs { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        ///  Tablo Konfigurasyonları entities/configurations klasöründe yapılır
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);



    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

}
