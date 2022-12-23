namespace TheaterLaakBackend.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string? Website { get; set; }

    public List<Artist> Artists { get; } = new();
    public List<Program> Programs { get; } = new();
}