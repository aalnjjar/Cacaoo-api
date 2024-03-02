using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IProductTypeService
{
    SM_Product_Types CreateType(SM_Product_Types typeDM);
    SM_Product_Types? GetType(int type_id);
}