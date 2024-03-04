using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IProductService
{
    SM_Products CreateProduct(SM_Products productDM);
    SM_Products UpdateProductByAdmin(SM_Products productDM);
    SM_Products? GetProduct(long product_id, long app_user_id = 0);
    AppProducts GetAppProducts(ProductRequest itemRequest);
    SM_Product_AddOns CreateProductAddOn(SM_Product_AddOns invoiceDM);
    List<SM_Product_AddOns> GetAllProductAddOns(long product_id);
    List<AddOnDTO> GetProductAddOns(long product_id, string lang = "E");
    SM_Product_AddOns? GetProductAddOn(long product_addon_id);
    bool DeleteProductAddOn(SM_Product_AddOns docDM);
    ProductDetailResponse GetProductDetail(long product_id);
    SM_Product_Occasions CreateProductOccasion(SM_Product_Occasions invoiceDM);
    bool DeleteProductOccasions(long product_id);
    List<SM_Product_Occasions> GetAllProductOccasions(long product_id);
    SM_Product_Branches CreateProductBranch(SM_Product_Branches invoiceDM);
    List<SM_Product_Branches> GetAllProductBranches(long product_id, long restaurant_id);
    SM_Product_Catering_Products CreateProductCategoryProduct(SM_Product_Catering_Products invoiceDM);
    List<CateringCategoryDTO> GetProductCateringProducts(long product_id, string lang = "E");
    SM_Catering_Categories? GetCateringCategory(long category_id);
    SM_Product_Catering_Products? GetProductCateringProduct(long catering_product_id);
    bool DeleteProductCateringProduct(SM_Product_Catering_Products docDM);
    List<SM_Product_Catering_Products> GetAllProductCateringProducts(long product_id);
}