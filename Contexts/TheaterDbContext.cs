using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Models;

namespace TheaterLaakBackend.Contexts;

//Dit heb ik gemaakt met de hulp van https://learn.microsoft.com/en-us/ef/
public abstract class TheaterDbContext : IdentityDbContext
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
    public DbSet<SeatShowStatus> SeatShowStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Models.Program>().
            HasMany(program => program.Genres).
            WithMany(genre => genre.Programs).
            UsingEntity(pivot => pivot.ToTable("GenreProgram"));

        builder.Entity<Models.Program>()
            .HasMany(it => it.Shows)
            .WithOne(it => it.Program)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Seat>()
            .HasMany(s => s.Shows)
            .WithMany(s => s.Seats)
            .UsingEntity<SeatShowStatus>(
                j => j
                    .HasOne(pt => pt.Show)
                    .WithMany(t => t.SeatShowStatus)
                    .HasForeignKey(pt => pt.ShowId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Seat)
                    .WithMany(t => t.SeatShowStatus)
                    .HasForeignKey(pt => pt.SeatId),
                j => 
                {
                    j.Property(pt => pt.Status).HasDefaultValueSql("'Available'");
                    j.HasKey(t => new { t.ShowId, t.SeatId });
                }

            );
        
    }
}

//TODO: verdeling in Services maken in SQL model
