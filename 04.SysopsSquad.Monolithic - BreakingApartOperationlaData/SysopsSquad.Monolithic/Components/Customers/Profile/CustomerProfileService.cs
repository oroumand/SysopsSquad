using SysopsSquad.Monolithic.Data;
using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Components.Customers
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly SysopsSquadDbContext _context;

        public CustomerProfileService(SysopsSquadDbContext context)
        {
            _context = context;
        }

        public Customer RegisterCustomer(string name, string email)
        {
            Console.WriteLine("Registering new customer...");
            var customer = new Customer { Name = name, Email = email };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}