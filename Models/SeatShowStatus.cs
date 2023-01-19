namespace TheaterLaakBackend.Models;

public class SeatShowStatus
{
    //Should be ["Available", "Occupied"]
    public string Status { get; set; }

    public int SeatId { get; set; }
    public Seat Seat { get; set; } 
    
    public int ShowId { get; set; }
    public Show Show { get; set; }
}