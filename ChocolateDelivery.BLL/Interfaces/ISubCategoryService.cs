using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface ISubCategoryService
{
    SM_Sub_Categories CreateSubCategory(SM_Sub_Categories categoryDM);
    SM_Sub_Categories? GetSubCategory(int category_id);
    List<SM_Sub_Categories> GetAllSubCategories();
}