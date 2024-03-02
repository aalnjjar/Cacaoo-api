using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IListService
{
    SM_LISTS? GetList(long list_id);
    List<SM_LIST_FIELDS> GetListFields(long list_id);
    string getDataField(SM_LIST_FIELDS findDetail, bool? is_StoreProcedure);
    List<SM_LIST_PARAMETERS> GetListParameters(long list_id);
}