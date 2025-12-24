namespace SysopsSquad.Monolithic.Components.Reporting
{
    public class ReportData
    {
        public int Id { get; set; } // Only one row in this table
        public int TotalCustomers { get; set; }
        public int TotalTickets { get; set; }
        public int CompletedTickets { get; set; }

    }
}
