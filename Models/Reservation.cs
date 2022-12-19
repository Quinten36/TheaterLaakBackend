namespace TheaterLaakBackend.Models;

public class Reservation
{
    public int id { get; set; }
    public int account { get; set; }
    public int hall { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public bool hasPaid { get; set; }
}