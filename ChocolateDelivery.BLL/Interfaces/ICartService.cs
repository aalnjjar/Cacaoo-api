using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface ICartService
{
    TXN_Cart AddtoCart(TXN_Cart CustomerDM);
    bool RemoveCartItem(long cart_id);
    CartResponse GetCartItems(long app_user_id, string lang);
    TXN_Cart_AddOns CreateCartAddOn(TXN_Cart_AddOns invoiceDM);
    TXN_Cart? GetCart(long cart_id);
    TXN_Cart? GetCart(long app_user_id, long product_id);
    bool IsValidVendor(long app_user_id, long product_id);
    bool IsValidCategory(long app_user_id, long product_id);
    bool RemoveCart(long app_user_id);
    TXN_Cart_Catering_Products CreateCartCateringProduct(TXN_Cart_Catering_Products invoiceDM);
}