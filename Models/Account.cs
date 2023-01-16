namespace TheaterLaakBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class Account : IdentityUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsDonator { get; set; }
    public bool IsSubscribed { get; set; }
    public bool isValidated { get; set; }
    public List<Order> Orders { get; } = new();
    public List<Genre> Intrests { get; } = new();
    public List<Reservation> Reservations { get; } = new();
}
//TODO: Fix spelfout Interests