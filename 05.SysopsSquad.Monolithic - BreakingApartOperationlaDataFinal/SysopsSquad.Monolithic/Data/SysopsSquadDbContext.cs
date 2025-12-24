using Microsoft.EntityFrameworkCore;
using SysopsSquad.Monolithic.Components.SupportContracts;
using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Data
{
    public class SysopsSquadDbContext : DbContext
    {
        public SysopsSquadDbContext(DbContextOptions<SysopsSquadDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<SysopsUser> SysopsUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<SupportContract> SupportContracts { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<ReplicatedCustomer> ReplicatedCustomersForContracts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships for the monolithic model
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedExpert)
                .WithMany()
                .HasForeignKey(t => t.AssignedExpertId);
        }
    }
}
