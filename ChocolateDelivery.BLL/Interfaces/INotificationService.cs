using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface INotificationService
{
    void SendNotificationToDriver(string order_no);
    APP_PUSH_CAMPAIGN CreatePushCampaign(APP_PUSH_CAMPAIGN CustomerDM);
    List<NOTIFICATION_INBOX> GetNotifications(long app_user_id,string lang = "E");
}