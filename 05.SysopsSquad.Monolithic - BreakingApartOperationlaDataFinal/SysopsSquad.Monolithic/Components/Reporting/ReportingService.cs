namespace SysopsSquad.Monolithic.Components.Reporting
{
    public class ReportingService : IReportingService
    {
        private readonly ReportingDbContext _context; // <-- تغییر کرد!

        public ReportingService(ReportingDbContext context) // <-- تغییر کرد!
        {
            _context = context;
        }

        public string GenerateOverallActivityReport()
        {
            Console.WriteLine("Generating report from PRIVATE reporting data...");

            // *** NO MORE COUPLING! Reads from its own replicated data. ***
            var reportData = _context.ReportAggregates.FirstOrDefault() ?? new ReportData();

            var report = $"--- Overall Activity Report (Decoupled) ---\n" +
                         $"Total Customers: {reportData.TotalCustomers}\n" +
                         $"Total Tickets Created: {reportData.TotalTickets}\n" +
                         $"Completed Tickets: {reportData.CompletedTickets}\n" + // (این فیلد را هم می‌توانید با رویداد TicketCompletedEvent پر کنید)
                         $"-----------------------------";

            Console.WriteLine(report);
            return report;
        }
    }
}