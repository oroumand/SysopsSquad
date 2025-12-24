using System.ComponentModel.DataAnnotations;

namespace SysopsSquad.Monolithic.Components.SupportContracts
{
    // A private, replicated copy of customer data needed by this component.
    public class ReplicatedCustomer
    {
        [Key]
        public int CustomerId { get; set; } // The original customer ID
        public string CustomerName { get; set; }
    }
}