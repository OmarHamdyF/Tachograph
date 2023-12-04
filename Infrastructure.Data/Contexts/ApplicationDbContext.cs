using Core.Domain.Entities;
using Infrastructure.Data.Contexts.seed;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts;
public class ApplicationDbContext : DbContext
{
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<TachographData> TachographData { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.DriversSeed();
    }

}

