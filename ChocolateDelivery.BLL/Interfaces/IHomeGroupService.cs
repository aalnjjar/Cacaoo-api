using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IHomeGroupService
{
    List<SM_Home_Groups> GetAllGroups();
    List<SM_Home_Groups> GetGroups();
    SM_Home_Groups? GetGroup(int Group_id);
    SM_Home_Groups? GetGroup(string Group_name);
    SM_Home_Groups CreateGroup(SM_Home_Groups CustomerDM);
    long GetMaxGroupId();
    SM_Home_Group_Details CreateGroupDetail(SM_Home_Group_Details CustomerDM);
    List<SM_Home_Group_Details> GetAllGroupDetails(long grp_id);
    List<SM_Home_Group_Details> GetGroupDetails(long grp_id);
    SM_Home_Group_Details? GetGroupDetail(long grp_detail_id);
    bool RemoveGroupdetail(long grp_detail_id);
    bool RemoveGroup(long grp_id);
    SM_Home_Groups? UpdateSequence(int grp_id, int sequence);
    string GetGroupType(short group_type_id);
    List<Select2DTO> GetGroupItems(short group_type_id);
    void UpdateGroupDetails(int line_no, long grp_id);
}