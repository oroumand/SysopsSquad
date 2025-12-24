using Microsoft.EntityFrameworkCore;
using SysopsSquad.Monolithic.Data;
using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Components.SupportContracts
{

    public class SupportContractService : ISupportContractService
    {
        private readonly SysopsSquadDbContext _context;

        public SupportContractService(SysopsSquadDbContext context)
        {
            _context = context;
        }

        public SupportContract CreateContract(int customerId, string productName, DateTime endDate)
        {
            // PROBLEM 1: Direct check on another component's table for data validation.
            var customerExists = _context.ReplicatedCustomersForContracts.Any(c => c.CustomerId == customerId);
            if (!customerExists)
            {
                throw new InvalidOperationException("Customer not found.");
            }

            var contract = new SupportContract
            {
                CustomerId = customerId,
                ProductName = productName,
                StartDate = DateTime.UtcNow,
                EndDate = endDate
            };

            _context.SupportContracts.Add(contract);
            _context.SaveChanges();
            return contract;
        }

        public ContractDetails? GetContractDetails(int contractId)
        {
            // PROBLEM 2: Direct JOIN between tables of different components.
            // This is a massive structural coupling at the data layer.
            var contract = _context.SupportContracts
                .Where(sc => sc.Id == contractId)
                .Include(sc => sc.Customer) // <-- The JOIN
                .Select(sc => new ContractDetails(sc.Id, sc.ProductName, sc.Customer.Name, sc.EndDate))
                .FirstOrDefault();

            return contract;
        }
    }
}