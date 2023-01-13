namespace TheaterLaakBackend.Models;

public class Show
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double FirstClassPrice { get; set; }
    public double? SecondClassPrice { get; set; }
    public double? ThirdClassPrice { get; set; }
    
    public int HallId { get; set; }
    public Hall Hall { get; set; }
    
    public int ProgramId { get; set; }
    public Program Program { get; set; }
    
    public int GroupId { get; set; }
    public Group Group { get; set; }
}