using SysopsSquad.Monolithic.Data;
using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Components.Tickets
{
    public class TicketAssignService : ITicketAssignService
    {
        private readonly SysopsSquadDbContext _context;

        public TicketAssignService(SysopsSquadDbContext context)
        {
            _context = context;
        }

        public void AssignTicket(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket != null && ticket.Status == "New")
            {
                // In a monolith, we can directly query data from another domain (Expert/User data)
                var expert = _context.SysopsUsers
                    .FirstOrDefault(u => u.Role == "Expert" && u.IsAvailable);

                if (expert != null)
                {
                    Console.WriteLine($"Assigning ticket {ticketId} to expert {expert.Username}...");
                    ticket.Status = "Assigned";
                    ticket.AssignedExpertId = expert.Id;
                    _context.SaveChanges();
                    // Here we would call TicketRoute and TicketNotify services...
                }
            }
        }
    }
}