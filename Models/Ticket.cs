namespace TheaterLaakBackend.Models;

public class Ticket
{
    public int Id { get; set; }
    
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    
    public int AccountId { get; set; }
    public Account Account { get; set; }
    
    public int ShowId { get; set; }
    public Show Show { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
}