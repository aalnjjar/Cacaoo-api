using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IAreaService
{
    List<SM_Areas> GetAllAreas();
    List<SM_Areas> GetAreas();
}