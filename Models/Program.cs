namespace TheaterLaakBackend.Models;

public class Program
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public bool isExclusive { get; set; }
    public DateTime beginExclusiveSale { get; set; }
    public DateTime beginSale { get; set; }
}