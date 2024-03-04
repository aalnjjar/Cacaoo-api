using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface ISettingService
{
    List<APP_SETTINGS> GetSettings();
    APP_SETTINGS? GetSetting(string Setting_Name);
    APP_SETTINGS SaveSetting(APP_SETTINGS userDM);
    T GetSettingValue<T>(string settingName);
}