namespace TheaterLaakBackend.Models;

public class Order
{
    public int Id { get; set; }
    public bool HasPaid { get; set; }

    public string? AccountId { get; set; }
    public Account? Account { get; set; }

    public List<Ticket> Tickets { get; } = new();
}

//TODO: HasPaid verplaatsen naar Order in SQL diagram