using System.Data;
using ChocolateDelivery.DAL;
using MySqlConnector;

namespace ChocolateDelivery.BLL;

public interface ICommonService
{
    List<SM_LABELS> GetAllLabels();
    List<SM_LABELS> GetAppLabels();
    SM_LABELS? GetLabel(int label_id);
    DataTable GetDataTable(long report_id, string stmt, List<string> paramterValues, string connectionString);
    MySqlDbType GetSqlDbType(string par_type);
    bool SendMail(string to, string subject, string body, string cc, string bcc, string fromEmail, string password);
    TXN_Favorite AddFavorite(TXN_Favorite CustomerDM);
    TXN_Favorite? GetFavorite(long app_user_id,long prod_id);
    bool RemoveFavorite(long fav_id);
    AppProducts GetFavoriteProducts(long app_user_id);
    T GetSettingValue<T>(string settingName);
    List<APP_SETTINGS> GetSettings();
}