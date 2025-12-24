using SysopsSquad.Monolithic.Models;

namespace SysopsSquad.Monolithic.Components.SupportContracts
{
    public interface ISupportContractService
    {
        SupportContract CreateContract(int customerId, string productName, DateTime endDate);
        ContractDetails? GetContractDetails(int contractId);
    }
}