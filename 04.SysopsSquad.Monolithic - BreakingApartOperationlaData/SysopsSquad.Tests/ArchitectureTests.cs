using NetArchTest.Rules;
using SysopsSquad.Monolithic.Components.SupportContracts; 
using SysopsSquad.Monolithic.Components.Customers; 
namespace SysopsSquad.Tests
{
    public class ArchitectureTests
    {
        private const string ReportingNamespace = "SysopsSquad.Monolithic.Components.Reporting";
        private const string TicketNamespace = "SysopsSquad.Monolithic.Components.Tickets";
        private const string CustomerNamespace = "SysopsSquad.Monolithic.Components.Customers";
        private const string BillingNamespace = "SysopsSquad.Monolithic.Components.Billing";


        private const string TicketModel = "SysopsSquad.Monolithic.Models.Ticket";
        
        private const string CustomersModel = "SysopsSquad.Monolithic.Models.Customer";


        //This is our Fitness Function!
        [Fact]
        public void Reporting_Should_Not_Have_Direct_Dependency_On_Other_Components()
        {
            // Arrange: Define the rule of our architecture.
            var result = Types.InNamespace(ReportingNamespace)
                .ShouldNot()
                .HaveDependencyOn(TicketNamespace)
                .And()
                .HaveDependencyOn(TicketModel)
                .And()
                .HaveDependencyOn(CustomerNamespace)
                .And()
                .HaveDependencyOn(CustomersModel)
                .And()
                .HaveDependencyOn(BillingNamespace)
                .GetResult();

            // Assert: Check if the rule is violated.
            Assert.True(result.IsSuccessful, "Reporting component has a forbidden dependency! " +
                result.FailingTypes?.FirstOrDefault()?.FullName);
        }

        [Fact]
        public void Reporting_Should_Not_Have_Direct_Reference_To_Other_Domains_Data()
        {
            var reportingAssembly = typeof(Monolithic.Components.Reporting.ReportingService).Assembly;

            // Rule: Types in Reporting namespace should not directly reference concrete types from other domains.
            // A better way is to check dependencies between namespaces.
            var result = Types.InNamespace("SysopsSquad.Monolithic.Components.Reporting")
                .ShouldNot()
                .HaveDependencyOn("SysopsSquad.Monolithic.Models.Ticket") // Check for specific problematic types
                .And()
                .HaveDependencyOn("SysopsSquad.Monolithic.Models.Customer")
                .GetResult();

            Assert.True(result.IsSuccessful, "Reporting has a forbidden direct data dependency!");
        }    


        [Fact]
        public void SupportContract_Should_Not_Have_Direct_Dependency_On_Customer_Component()
        {


            var reportingAssembly = typeof(SupportContractService).Assembly;

            // Rule: Types in the SupportContract component should not have direct reference
            // to types (especially models) in the Customer component.
            var result = Types.InNamespace("SysopsSquad.Monolithic.Components.SupportContracts")
                .ShouldNot()
                .HaveDependencyOn("SysopsSquad.Monolithic.Components.Customers") // Check for specific problematic types
                .And()
                .HaveDependencyOn("SysopsSquad.Monolithic.Models.Customer")
                .GetResult();

            Assert.True(result.IsSuccessful, "SupportContract has a forbidden direct dependency on the Customer component!");
        }
    }
}
