using SysopsSquad.Monolithic.Data;
using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Components.Reporting
{
    public class ReportingService : IReportingService
    {
        private readonly SysopsSquadDbContext _context;

        public ReportingService(SysopsSquadDbContext context)
        {
            _context = context;
        }

        public string GenerateOverallActivityReport()
        {
            Console.WriteLine("Generating overall activity report...");

            // THE CORE PROBLEM: Direct data access to other components' tables.
            // This creates massive structural coupling.
            List<Ticket> tickets = _context.Tickets.ToList();
            var ticketCount = tickets.Count();
            var customerCount = _context.Customers.Count();
            var totalBillings = _context.Billings.Sum(b => b.Amount);
            var completedTickets = _context.Tickets.Count(t => t.Status == "Completed");

            var report = $"--- Overall Activity Report ---\n" +
                         $"Total Customers: {customerCount}\n" +
                         $"Total Tickets Created: {ticketCount}\n" +
                         $"Completed Tickets: {completedTickets}\n" +
                         $"Total Billings Amount: ${totalBillings:F2}\n" +
                         $"-----------------------------";

            Console.WriteLine(report);
            return report;
        }
    }
}