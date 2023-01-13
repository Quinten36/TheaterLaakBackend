namespace TheaterLaakBackend.Models;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsDonator { get; set; }
    public bool IsSubscribed { get; set;}

    public List<Order> Orders { get; } = new();
    public List<Genre> Intrests { get; } = new();
    public List<Reservation> Reservations { get; } = new();
    public bool isValidated { get; set; }
}