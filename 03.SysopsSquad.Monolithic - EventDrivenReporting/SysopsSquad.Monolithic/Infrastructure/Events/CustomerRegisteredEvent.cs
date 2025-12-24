namespace SysopsSquad.Monolithic.Infrastructure.Events
{
    public record CustomerRegisteredEvent(int CustomerId, string CustomerName);
}