namespace SysopsSquad.Monolithic.Models;

public class Billing
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }
    public DateTime BillingDate { get; set; }
    public bool IsPaid { get; set; }
}
