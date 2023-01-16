namespace TheaterLaakBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Account : IdentityUser
{
    public string Password { get; set; }
    // public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsDonator { get; set; }
    public bool IsSubscribed { get; set; }
    public bool isValidated { get; set; }
    public List<Order> Orders { get; } = new();
    public List<Genre> Intrests { get; } = new();
    public List<Reservation> Reservations { get; } = new();

    public Account() {}

    [JsonConstructor]
    public Account (string userName, string password, string email, string phoneNumber, bool _IsDonator, bool _IsSubscribed) {
      UserName = userName;
      Password = password;
      Email = email;
      PhoneNumber = phoneNumber;
      IsDonator = _IsDonator;
      IsSubscribed = _IsSubscribed;
    }
}
//TODO: Fix spelfout Interests