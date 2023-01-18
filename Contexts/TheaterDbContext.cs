using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TheaterLaakBackend.Controllers;


//Dit heb ik gemaakt met de hulp van https://learn.microsoft.com/en-us/ef/
public class TheaterDbContext : IdentityDbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<TheaterLaakBackend.Models.Program> Programs { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Validation> Verificaties { get; set; }
    public DbSet<FeedbackDonateurs> FeedbackDonateurs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.db");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Models.Program>().
            HasMany(program => program.Genres).
            WithMany(genre => genre.Programs).
            UsingEntity(pivot => pivot.ToTable("GenreProgram"));
    }
}

//TODO: verdeling in Services maken in SQL model

//TODO: Translate to English
