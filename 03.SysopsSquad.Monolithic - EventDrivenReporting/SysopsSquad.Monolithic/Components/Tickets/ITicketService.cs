using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Components.Tickets
{
    // A monolithic service doing many things: creation, completion, etc.
    public interface ITicketService
    {
        Ticket CreateTicket(string description, int customerId);
        void CompleteTicket(int ticketId);
    }
}
