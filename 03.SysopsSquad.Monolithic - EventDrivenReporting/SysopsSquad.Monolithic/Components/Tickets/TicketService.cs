using SysopsSquad.Monolithic.Components.Surveies.Notifies;
using SysopsSquad.Monolithic.Data;
using SysopsSquad.Monolithic.Infrastructure;
using SysopsSquad.Monolithic.Infrastructure.Events;
using SysopsSquad.Monolithic.Models;
namespace SysopsSquad.Monolithic.Components.Tickets
{

    public class TicketService : ITicketService
    {
        private readonly SysopsSquadDbContext _context;
        // In a monolith, we can directly inject other services. This is a key problem to discuss.
        private readonly ITicketAssignService _ticketAssignService;
        private readonly ISurveyNotifyService _surveyNotifyService;
        private readonly IEventBus _eventBus;
        public TicketService(
            SysopsSquadDbContext context,
            ITicketAssignService ticketAssignService,
            ISurveyNotifyService surveyNotifyService,
            IEventBus eventBus)
        {
            _context = context;
            _ticketAssignService = ticketAssignService;
            _surveyNotifyService = surveyNotifyService;
            _eventBus = eventBus;
        }

        public Ticket CreateTicket(string description, int customerId)
        {
            Console.WriteLine("Creating new ticket...");
            var ticket = new Ticket
            {
                Description = description,
                CustomerId = customerId,
                Status = "New",
                CreatedDate = DateTime.UtcNow
            };
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            // *** PUBLISH EVENT ***
            Console.WriteLine("Publishing TicketCreatedEvent...");
            var ticketCreatedEvent = new TicketCreatedEvent(ticket.Id, ticket.CustomerId, ticket.CreatedDate);
            _eventBus.Publish(ticketCreatedEvent);

            // Tightly coupled call to another component's logic
            _ticketAssignService.AssignTicket(ticket.Id);

            return ticket;
        }

        // ... (متد CompleteTicket هم می‌تواند رویداد منتشر کند)
        public void CompleteTicket(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket != null)
            {
                Console.WriteLine($"Completing ticket {ticketId}...");
                ticket.Status = "Completed";
                _context.SaveChanges();

                // Tightly coupled call to notify for survey
                _surveyNotifyService.SendSurveyEmail(ticket.CustomerId, ticket.Id);
            }
        }
    }
}
