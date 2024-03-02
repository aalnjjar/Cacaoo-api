using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IRedeemPointService
{
    LP_POINTS_TRANSACTION CreatePointTransaction(LP_POINTS_TRANSACTION pointDM);
}