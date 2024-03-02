using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface ICarouselService
{
    List<SM_Carousels> GetAllCarousels();
    List<SM_Carousels> GetCarousels();
    SM_Carousels? GetCarousel(int Carousel_Id);
    SM_Carousels CreateCarousel(SM_Carousels CustomerDM);
    bool RemoveCarousel(SM_Carousels CustomerDM);
    long GetMaxCarouselId();
    SM_Carousels? UpdateSequence(long carousel_id, int sequence);
}