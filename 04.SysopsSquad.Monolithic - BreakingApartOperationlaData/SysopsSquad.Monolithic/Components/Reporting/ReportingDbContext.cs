using Microsoft.EntityFrameworkCore;

namespace SysopsSquad.Monolithic.Components.Reporting
{
    public class ReportingDbContext : DbContext
    {
        public ReportingDbContext(DbContextOptions<ReportingDbContext> options) : base(options) { }

        public DbSet<ReportData> ReportAggregates { get; set; }
    }
}
