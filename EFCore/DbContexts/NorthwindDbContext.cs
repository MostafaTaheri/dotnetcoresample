using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

public class NorthwindDbContext : DbContext
{
    private readonly string _connectionString;

    public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) :
         base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies(); //Set Lazy loading
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasIndex(entity => entity.CategoryName);
        modelBuilder.Entity<Category>().HasQueryFilter(entity => !entity.IsDeleted);

        modelBuilder.Entity<Product>().HasIndex(entity => entity.ProductName);
        modelBuilder.Entity<Product>().HasQueryFilter(entity => !entity.IsDeleted);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder
        configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(50);
        //configurationBuilder.IgnoreAny<Category>();
    }
}