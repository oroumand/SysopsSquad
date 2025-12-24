using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Components.Customers
{
    public interface ICustomerProfileService
    {
        Customer RegisterCustomer(string name, string email);
    }
}