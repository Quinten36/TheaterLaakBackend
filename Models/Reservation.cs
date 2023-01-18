namespace TheaterLaakBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Reservation
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool HasPaid { get; set; }
    
    public int AccountId { get; set; }
    public Account Account { get; set; }
    
    public int HallId { get; set; }
    public Hall Hall { get; set; }

    [NotMapped]
    public int? capacity { get; set; }
}

//TODO: Reservering veranderen naar Reservation in SQL model
//TODO: accountID1 uit db verwijderen
//TODO: fix foreign key accountId