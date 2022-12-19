namespace TheaterLaakBackend.Models;

public class Account
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string? phoneNumber { get; set; }
    public bool isDonateur { get; set; }
    public bool isSubscribed { get; set; }
}