using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IOccasionService
{
    SM_Occasions CreateOccasion(SM_Occasions categoryDM);
    SM_Occasions? GetOccasion(int category_id);
    List<SM_Occasions> GetOccasions();
}