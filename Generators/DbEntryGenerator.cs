using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using TheaterLaakBackend.Controllers;
using TheaterLaakBackend.Models;


namespace TheaterLaakBackend.Generators;

public class DbEntryGenerator
{
    private readonly TheaterDbContext _context;
    private readonly Faker _faker = new Faker("en");
    
    public DbEntryGenerator(TheaterDbContext context)
    {
        _context = context;
    }
    
    public void AccountGenerator()
    {
        for (int i = 0; i < 100; i++)
        {
            
            var account = new Account
            {
                Username = _faker.Name.FullName(),
                Password = _faker.Internet.Password(),
                Email = _faker.Internet.Email(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
                IsDonator = _faker.Random.Bool(),
                IsSubscribed = _faker.Random.Bool()
            };
            _context.Accounts.Add(account);
        }
        _context.SaveChanges();
    }

    public void ArtistGenerator()
    {
        for (int i = 0; i < 100; i++)
        {
            
            var artist = new Artist
            {
                Name = _faker.Name.FullName(),
            };
            _context.Artists.Add(artist);
        }
        _context.SaveChanges();
    }

    public void GenreGenerator()
    {
        var genres = new string[]
            { "Horror", "Sport", "Actie", "Musical", "Concert", "Adventure", "Stageplay", "Caberet" };
        for (int i = 0; i < genres.Length; i++)
        {
            
            var genre  = new Genre
            {
                Name = genres[i]
            };
            _context.Genres.Add(genre);
        }
        _context.SaveChanges();
    }
    
    public void GroupGenerator()
    {
        for (int i = 0; i < 20; i++)
        {
            
            var group  = new Group
            {
                Name = _faker.Hacker.Noun() + " " + _faker.Hacker.Verb() + " " + _faker.Hacker.Noun(),
                Logo = _faker.Image.PicsumUrl(),
                Website = _faker.Internet.Url()
            };
            
            _context.Groups.Add(group);
        }
        _context.SaveChanges();
    }
    
    public void HallGenerator()
    {
        for (int i = 0; i < 10; i++)
        {
            
            var hall  = new Hall
            {
                Capacity = _faker.Random.Int(0, 200)
            };
            
            _context.Halls.Add(hall);
        }
        _context.SaveChanges();
    }
    
    public void OrderGenerator()
    {
        for (int i = 0; i < 20; i++)
        {

            var accounts = _context.Accounts;
            var order  = new Order
            {
                HasPaid = _faker.Random.Bool(),
                AccountId = _faker.Random.Int(1, accounts.Count())
            };
            
            _context.Orders.Add(order);
        }
        _context.SaveChanges();
    }
    
    public void ProgramGenerator()
    {
        for (int i = 0; i < 20; i++)
        {

            var groups = _context.Groups;
            var beginExlusiveSale = _faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now);
            var program  = new Models.Program
            {
                Title = _faker.Hacker.Noun() + " " + _faker.Hacker.Verb() + " " + _faker.Hacker.Noun(),
                Description = _faker.Lorem.Paragraph(),
                IsExclusive = _faker.Random.Bool(),
                Image = _faker.Image.PicsumUrl(),
                BeginExclusiveSale = beginExlusiveSale,
                BeginSale = beginExlusiveSale.AddDays(14),
                GroupId = _faker.Random.Int(1, groups.Count())
            };
            
            _context.Programs.Add(program);
        }
        _context.SaveChanges();
    }
    
    public void ReservationGenerator()
    {
        for (int i = 0; i < 20; i++)
        {

            var accounts = _context.Accounts;
            var halls = _context.Halls;
            var start = _faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now );
            var reservation  = new Reservation()
            {
                Start = start,
                End = start.AddHours(2),
                HasPaid = _faker.Random.Bool(),
                AccountId = _faker.Random.Int(1, accounts.Count()),
                HallId = _faker.Random.Int(1, halls.Count())
                
            };
            
            _context.Reservations.Add(reservation);
        }
        _context.SaveChanges();
    }
    
    public void SeatGenerator()
    {
        for (int i = 0; i < 20; i++)
        {
            
            var halls = _context.Halls;
            var seat  = new Seat
            {
                Row = _faker.Random.Int(1, 20),
                SeatNumber = _faker.Random.Int(1, 20),
                ForDisabled = _faker.Random.Bool(),
                SeatClass = _faker.Random.Int(1, 3),
                HallId = _faker.Random.Int(1, halls.Count())

            };
            
            _context.Seats.Add(seat);
        }
        _context.SaveChanges();
    }
    
    public void ShowGenerator()
    {
        for (int i = 0; i < 20; i++)
        {

            var halls = _context.Halls;
            var program = _context.Programs;
            var groups = _context.Groups;

            var start = _faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now );
            var show = new Show()
            {
                Start = start,
                End = start.AddHours(2),
                FirstClassPrice = _faker.Random.Double(0.0, 200.0),
                SecondClassPrice = _faker.Random.Double(0.0, 150.0),
                ThirdClassPrice = _faker.Random.Double(0.0, 100.0),
                HallId = _faker.Random.Int(1, halls.Count()),
                ProgramId = _faker.Random.Int(1, halls.Count()),
                GroupId = _faker.Random.Int(1, halls.Count())
            };
            
            _context.Shows.Add(show);
        }
        _context.SaveChanges();
    }

    public void TicketGenerator()
    {
        for (int i = 0; i < 20; i++)
        {

            var seats = _context.Seats;
            var accounts = _context.Accounts;
            var shows = _context.Shows;

            var start = _faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now );
            var ticket = new Ticket()
            {
                SeatId = _faker.Random.Int(1, seats.Count()),
                AccountId = _faker.Random.Int(1, accounts.Count()),
                ShowId = _faker.Random.Int(1, shows.Count())
            };
            
            _context.Tickets.Add(ticket);
        }
        _context.SaveChanges();
    }

    public void AccountGenrePivotGenerator()
    {
        var accounts = _context.Accounts;
        var genres = _context.Genres;
        
        foreach (var account in accounts)
        {
            var randomTotalGenres = _faker.Random.Int(0, 5);
            var usedGenreIds = new HashSet<int>();

            for (int i = 0; i < randomTotalGenres; i++)
            {
                int randomGenreId;
                do
                {
                    randomGenreId = _faker.Random.Int(1, genres.Count());
                }
                while (usedGenreIds.Contains(randomGenreId));

                usedGenreIds.Add(randomGenreId);
                account.Intrests.Add(genres.First(genre => genre.Id == randomGenreId));
            }
        }
        _context.SaveChanges();
    }
    
    public void GenreProgramPivotGenerator()
    {
        var programs = _context.Programs;
        var genres = _context.Genres;
        
        foreach (var program in programs)
        {
            var randomTotalGenres = _faker.Random.Int(0, 5);
            var usedGenreIds = new HashSet<int>();

            for (int i = 0; i < randomTotalGenres; i++)
            {
                int randomGenreId;
                do
                {
                    randomGenreId = _faker.Random.Int(1, genres.Count());
                }
                while (usedGenreIds.Contains(randomGenreId));

                usedGenreIds.Add(randomGenreId);
                program.Genres.Add(genres.First(genre => genre.Id == randomGenreId));
            }
        }
        _context.SaveChanges();
    }

    public void DatabaseGenerator()
    {
        AccountGenerator();
        ArtistGenerator();
        GenreGenerator();
        GroupGenerator();
        HallGenerator();
        OrderGenerator();
        ProgramGenerator();
        ReservationGenerator();
        SeatGenerator();
        ShowGenerator();
        TicketGenerator();
        AccountGenrePivotGenerator();
        GenreProgramPivotGenerator();
    }
    
}

// dotnet-aspnet-codegenerator controller -name TicketController -async -sqlite -api -m Ticket -dc TheaterDbContext -outDir Controllers
