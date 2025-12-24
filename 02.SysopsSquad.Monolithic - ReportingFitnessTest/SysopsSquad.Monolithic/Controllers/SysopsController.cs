using Microsoft.AspNetCore.Mvc;
using SysopsSquad.Monolithic.Components.Billing;
using SysopsSquad.Monolithic.Components.Customers;
using SysopsSquad.Monolithic.Components.Reporting;
using SysopsSquad.Monolithic.Components.Tickets;

namespace SysopsSquad.Monolithic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SysopsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ICustomerProfileService _customerProfileService;
        private readonly IBillingService _billingService;
        private readonly IReportingService _reportingService;
        // Problem: The controller is coupled to many different services from different domains.
        public SysopsController(
            ITicketService ticketService,
            ICustomerProfileService customerProfileService,
            IBillingService billingService,
             IReportingService reportingService)
        {
            _ticketService = ticketService;
            _customerProfileService = customerProfileService;
            _billingService = billingService;
            _reportingService = reportingService;
        }

        [HttpPost("ticket")]
        public IActionResult CreateTicket([FromBody] CreateTicketRequest request)
        {
            var ticket = _ticketService.CreateTicket(request.Description, request.CustomerId);
            return Ok(ticket);
        }

        [HttpPost("ticket/{id}/complete")]
        public IActionResult CompleteTicket(int id)
        {
            _ticketService.CompleteTicket(id);
            return Ok($"Ticket {id} marked as complete.");
        }

        [HttpPost("customer")]
        public IActionResult RegisterCustomer([FromBody] RegisterCustomerRequest request)
        {
            var customer = _customerProfileService.RegisterCustomer(request.Name, request.Email);
            return Ok(customer);
        }

        [HttpPost("billing/process")]
        public IActionResult ProcessBilling()
        {
            _billingService.ProcessMonthlyBilling();
            return Ok("Monthly billing processed.");
        }

        [HttpGet("report/activity")]
        public IActionResult GetActivityReport()
        {
            var report = _reportingService.GenerateOverallActivityReport();
            return Ok(report);
        }
    }

    // DTOs for requests
    public record CreateTicketRequest(string Description, int CustomerId);
    public record RegisterCustomerRequest(string Name, string Email);
}