namespace SysopsSquad.Monolithic.Models;

public class Survey
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int Rating { get; set; }
    public string Comments { get; set; }
}