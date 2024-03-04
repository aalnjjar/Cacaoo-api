using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface ICateringCategoryService
{
    SM_Catering_Categories CreateCategory(SM_Catering_Categories categoryDM);
    SM_Catering_Categories? GetCategory(int category_id);
    List<SM_Catering_Categories> GetCategories();
}