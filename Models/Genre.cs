namespace TheaterLaakBackend.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Account> Accounts { get; } = new();
    public List<Program> Programs { get; set; } = new();
    
}