using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IRestaurantService
{
    SM_Restaurants CreateRestaurant(SM_Restaurants restaurantDM);
    SM_Restaurants? GetRestaurant(long restaurant_id);
    SM_Restaurants? ValidateMerchant(string userName, string password);
    List<SM_Restaurant_AddOns> GetAllRestaurantAddOns(long restaurant_id);
}