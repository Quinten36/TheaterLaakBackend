namespace TheaterLaakBackend.Models;

public class Hall
{
    public int Id { get; set; }
    public int Capacity { get; set; }

    public List<Show> Shows { get; } = new();
    public List<Seat> Seats { get; } = new();
    public List<Reservation> Reservations { get; } = new();
}

//TODO: Toevoegen FirstClassSeats in sql diagram
//TODO: Toevoegen SecondClassSeats in sql diagram
//TODO: Toevoegen ThirdClassSeats in sql diagram
//TODO: aanpassen capacity in sql diagram moet null kunnen zijn