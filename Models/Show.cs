namespace TheaterLaakBackend.Models;

public class Show
{
    public int id { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public int hall { get; set; }
    public int program { get; set; }
    public int? band { get; set; }
    public double firstClassPrice { get; set; }
    public double? secondClassPrice { get; set; }
    public double? ThirdClassPrice { get; set; }
}