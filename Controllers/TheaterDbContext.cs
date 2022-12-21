using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Models;

namespace TheaterLaakBackend.Controllers;

public class TheaterDbContext : DbContext
{
    public TheaterDbContext(DbContextOptions<TheaterDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    public DbSet<Account> Account { get; set; }
    public DbSet<AccountTagPivot> AccountTagPivot { get; set; }
    public DbSet<Artist> Artist { get; set; }

}