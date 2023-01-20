using Microsoft.EntityFrameworkCore;

namespace TheaterLaakBackend.Contexts;

public class SqlServerTheaterDbContext : TheaterDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SQLAZURECONNSTR_defaultConnection"));
    }
}