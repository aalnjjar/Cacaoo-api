using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IAppUserService
{
    App_Users? GetAppUser(string email_id, short login_type);
    App_Users? GetAppUser(long app_user_id);
    App_Users CreateAppUser(App_Users PromoterDM);
    App_Users? ValidateAppUser(string email, string password);
    App_Users? GetAppUserByRowId(Guid row_id);
    App_User_Address CreateAppUserAddress(App_User_Address PromoterDM);
    App_User_Address? GetUserAddress(long address_id);
    List<App_User_Address> GetUserAddresses(long app_user_id,string lang = "E");
    bool DeleteUserAddress(long userAddressId);
}