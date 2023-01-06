namespace TheaterLaakBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;

[NotMapped]
public class ReservationFilter
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

}