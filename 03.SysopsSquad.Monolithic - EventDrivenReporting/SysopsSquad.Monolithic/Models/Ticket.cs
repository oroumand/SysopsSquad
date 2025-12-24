namespace SysopsSquad.Monolithic.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Status { get; set; } // "New", "Assigned", "InProgress", "Completed"
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int? AssignedExpertId { get; set; }
    public SysopsUser? AssignedExpert { get; set; }
}
