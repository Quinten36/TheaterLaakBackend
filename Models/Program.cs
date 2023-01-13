namespace TheaterLaakBackend.Models;

public class Program
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsExclusive { get; set; }
    public string Image { get; set; }
    public DateTime BeginExclusiveSale { get; set; }
    public DateTime BeginSale { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<Genre> Genres { get; } = new();

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public List<Show> Shows { get; } = new();
}

//TODO: price uit SQL model weghalen
//TODO: Image toevoegen in sql model
//TODO: Andere Naam?