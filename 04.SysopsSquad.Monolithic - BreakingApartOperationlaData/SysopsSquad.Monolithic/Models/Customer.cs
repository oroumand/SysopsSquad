namespace SysopsSquad.Monolithic.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ICollection<SupportContract> Contracts { get; set; } = new List<SupportContract>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
