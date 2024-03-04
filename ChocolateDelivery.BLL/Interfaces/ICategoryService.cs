using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface ICategoryService
{
    SM_Main_Categories CreateMainCategory(SM_Main_Categories categoryDM);
    SM_Main_Categories? GetMainCategory(int category_id);
    SM_Categories CreateCategory(SM_Categories categoryDM);
    SM_Categories? GetCategory(int category_id);
    List<SM_Categories> GetCategories();
    List<SM_Sub_Categories> GetSubCategories(long cat_id);
}