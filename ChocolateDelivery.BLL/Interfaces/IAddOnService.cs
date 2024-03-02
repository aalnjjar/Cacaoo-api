using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IAddOnService
{
    SM_Restaurant_AddOns CreateAddOn(SM_Restaurant_AddOns addonDM);
    SM_Restaurant_AddOns? GetAddOn(long addon_id);
}