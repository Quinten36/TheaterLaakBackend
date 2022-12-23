namespace TheaterLaakBackend.Models;

public class Seat
{
    public int Id { get; set; }
    public int Row{ get; set; }
    public int SeatNumber { get; set; }
    public bool ForDisabled { get; set; }
    public int SeatClass { get; set; }
    
    public int HallId { get; set; }
    public Hall Hall { get; set; }

    public List<Ticket> Tickets { get; } = new();
}