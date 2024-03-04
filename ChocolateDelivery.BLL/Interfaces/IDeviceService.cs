using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IDeviceService
{
    Device_Registration RegisterDevice(Device_Registration menu);
    Device_Registration Logout(Device_Registration menu);
    Device_Registration? GetDevice(string uniquedeviceid);
    Device_Registration? GetDeviceByClientKey(string clientkey);
    List<Device_Registration> GetTotalDownloads(DateTime FromDate, DateTime ToDate);
    List<Device_Registration> GetTotalMonthlyDownloads(DateTime FromDate, DateTime ToDate);
}