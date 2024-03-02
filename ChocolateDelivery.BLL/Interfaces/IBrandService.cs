using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IBrandService
{
    SM_Brands CreateBrand(SM_Brands brandDM);
    SM_Brands? GetBrand(int brand_id);
    List<SM_Restaurants> GetBrands(ProductRequest itemRequest, string lang = "E");
    List<SM_Sub_Categories> GetBrandCategories(long brand_id);
    List<SM_Products> GetBrandCategoryProducts(long brand_id, long cat_id);
}