using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IBranchService
{
    SM_Restaurant_Branches CreateBranch(SM_Restaurant_Branches brandDM);
    SM_Restaurant_Branches? GetBranch(int brand_id);
    List<SM_Restaurant_Branches> GetAllBranches(string lang = "E");
    List<SM_Restaurant_Branches> GetRestaurantBranches(long restaurant_id);
}