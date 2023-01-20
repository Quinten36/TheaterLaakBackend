using Microsoft.EntityFrameworkCore;

namespace TheaterLaakBackend.Contexts;

public class SqlLiteTheaterDbContext : TheaterDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.db");
        optionsBuilder.EnableSensitiveDataLogging();
    }
}