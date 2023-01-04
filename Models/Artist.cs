namespace TheaterLaakBackend.Models;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Group> Groups { get; } = new();
}

//TODO: SQL model many to many naar group (pivot)