using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IOrderService
{
    TXN_Orders SaveOrder(TXN_Orders CustomerDM);
    TXN_Order_Details SaveOrderDetail(TXN_Order_Details CustomerDM);
    TXN_Order_Detail_AddOns SaveOrderDetailAddon(TXN_Order_Detail_AddOns CustomerDM);
    TXN_Order_Detail_Catering_Products SaveOrderDetailCateringProduct(TXN_Order_Detail_Catering_Products CustomerDM);
    TXN_Orders? GetOrder(long order_id, string lang = "E");
    TXN_Order_Details? GetOrderDetail(long order_detail_id);
    TXN_Orders? GetOrderByOrderDetail(long order_detail_id);
    List<TXN_Order_Details> GetOrderDetails(long order_id);
    List<TXN_Order_Detail_AddOns> GetOrderDetailAddOns(long order_detail_id);
    List<TXN_Order_Detail_Catering_Products> GetOrderDetailCategoryProducts(long order_detail_id,string lang ="E");
    long GetNextOrderNo();
    TXN_Order_Logs CreateOrderLog(TXN_Order_Logs CustomerDM);
    Site_Configuration? GetSiteConfiguration(string config_name);
    List<SM_Payment_Types> GetPaymentTypes();
    PAYMENTS CreatePayment(PAYMENTS paymentDM);
    PAYMENTS? GetPaymentByTrackId(string track_id);
    PAYMENTS? GetPaymentByOrderId(long order_id);
    List<TXN_Orders> GetPendingDriverOrders(string lang, long driver_id);
    TXN_Orders? GetExistingDriverOrder(long driver_id);
    List<TXN_Orders> GetDeliveredDriverOrders(string lang, long driver_id);
    List<TXN_Orders> GetDeliveredDriverOrders(string lang, long driver_id, DateTime from_date, DateTime to_date);
    List<TXN_Orders> GetCustomerOrders(string lang, long app_user_id);
    List<TXN_Orders> GetReportOrders(DateTime FromDate, DateTime ToDate, string lang);
    List<TXN_Order_Details> GetReportOrderDetails(DateTime FromDate, DateTime ToDate, string lang);
    List<DriverOrderDTO> GetDashboardOrders(int dashboard_order_type);
    TXN_Order_Tracking_Details CreateOrderTrackingDetail(TXN_Order_Tracking_Details CustomerDM);
    TXN_Order_Tracking_Details? GetLatestTrackingDetail(long order_id);
}