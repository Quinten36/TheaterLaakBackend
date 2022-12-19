namespace TheaterLaakBackend.Models;

public class Seat
{
    public int id { get; set; }
    public int row{ get; set; }
    public int seat { get; set; }
    public int hall { get; set; }
    public bool forDisabled { get; set; }
    public int seatClass { get; set; }
}