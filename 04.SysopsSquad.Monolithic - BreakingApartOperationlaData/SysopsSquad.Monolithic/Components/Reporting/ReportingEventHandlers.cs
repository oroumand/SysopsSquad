using Microsoft.EntityFrameworkCore;
using SysopsSquad.Monolithic.Infrastructure.Events;

namespace SysopsSquad.Monolithic.Components.Reporting
{
    public class ReportingEventHandlers
    {
        private readonly ReportingDbContext _context;

        // Using DbContextFactory is a good practice for long-running/background services
        public ReportingEventHandlers(ReportingDbContext context)
        {
            _context = context;
        }

        public async Task Handle(TicketCreatedEvent @event)
        {
            Console.WriteLine("Reporting component received TicketCreatedEvent.");
            var reportData = await _context.ReportAggregates.FirstOrDefaultAsync();
            if (reportData == null)
            {
                reportData = new ReportData { Id = 1 };
                _context.ReportAggregates.Add(reportData);
            }
            reportData.TotalTickets++;
            await _context.SaveChangesAsync();
        }

        public async Task Handle(CustomerRegisteredEvent @event)
        {
            Console.WriteLine("Reporting component received CustomerRegisteredEvent.");
            var reportData = await _context.ReportAggregates.FirstOrDefaultAsync();
            if (reportData == null)
            {
                reportData = new ReportData { Id = 1 };
                _context.ReportAggregates.Add(reportData);
            }
            reportData.TotalCustomers++;
            await _context.SaveChangesAsync();
        }
    }
}
