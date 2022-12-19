namespace TheaterLaakBackend.Models;

public class Ticket
{
    public int id { get; set; }
    public int seat { get; set; }
    public bool hasPaid { get; set; }
    public int account { get; set; }
    public int show { get; set; }
}