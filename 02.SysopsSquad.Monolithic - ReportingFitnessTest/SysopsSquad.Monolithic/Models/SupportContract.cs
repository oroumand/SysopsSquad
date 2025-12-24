namespace SysopsSquad.Monolithic.Models;

public class SupportContract
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
