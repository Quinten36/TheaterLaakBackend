using Microsoft.EntityFrameworkCore;

namespace TheaterLaakBackend.Controllers;

public class TheaterDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }
}