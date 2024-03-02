using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IChefService
{
    SM_Chefs CreateChef(SM_Chefs categoryDM);
    SM_Chefs? GetChef(int chef_id);
    SM_Chef_Products CreateChefProduct(SM_Chef_Products invoiceDM);
    List<SM_Chef_Products> GetAllChefProducts(long chef_id);
    AppProducts GetChefProducts(long chef_id);
    bool DeleteChefProduct(long detail_id);
}