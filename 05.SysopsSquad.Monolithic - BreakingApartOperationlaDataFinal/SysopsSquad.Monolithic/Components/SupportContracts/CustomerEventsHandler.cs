using Microsoft.EntityFrameworkCore;
using SysopsSquad.Monolithic.Data;
using SysopsSquad.Monolithic.Infrastructure.Events;

namespace SysopsSquad.Monolithic.Components.SupportContracts
{
    public class CustomerEventsHandler 
    {
        private readonly SysopsSquadDbContext _context;

        public CustomerEventsHandler(SysopsSquadDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CustomerRegisteredEvent @event)
        {
            Console.WriteLine($"SupportContract component received CustomerRegisteredEvent for customer {@event.CustomerId}");

            var replicatedCustomer = new ReplicatedCustomer
            {
                CustomerId = @event.CustomerId,
                CustomerName = @event.CustomerName
            };

            _context.ReplicatedCustomersForContracts.Add(replicatedCustomer);
            await _context.SaveChangesAsync();
        }


    }
}