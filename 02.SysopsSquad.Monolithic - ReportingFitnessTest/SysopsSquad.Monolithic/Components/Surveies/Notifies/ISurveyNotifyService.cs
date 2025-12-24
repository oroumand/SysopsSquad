namespace SysopsSquad.Monolithic.Components.Surveies.Notifies
{
    public interface ISurveyNotifyService
    {
        void SendSurveyEmail(int customerId, int ticketId);
    }
}