namespace SysopsSquad.Monolithic.Components.Surveies.Notifies
{

    public class SurveyNotifyService : ISurveyNotifyService
    {
        public void SendSurveyEmail(int customerId, int ticketId)
        {
            Console.WriteLine($"Sending survey email to customer {customerId} for ticket {ticketId}.");
            // Logic to send email would go here
        }
    }
}