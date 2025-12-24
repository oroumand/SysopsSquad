namespace SysopsSquad.Monolithic.Infrastructure.Events
{
    public record TicketCreatedEvent(int TicketId, int CustomerId, DateTime CreatedDate);
}