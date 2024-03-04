using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public interface IUserService
{
    SM_USERS? ValidateUser(string userName, string password);
    SM_USERS? isUserExist(string userName);
    List<SM_USERS> GetUsers();
    List<SM_USERS> GetProductionUsers();
    SM_USERS? GetUser(Int16 userCd);
    List<SM_MENU> GetUserMenu(int userId);
    List<SM_MENU> GetUserMenuEditPerm(int userId);
    List<SM_MENU> GetUserMenuAddPerm(int userId);
    List<SM_MENU> GetUserMenuDeletePerm(int userId);
    List<SM_MENU> GetAllMenus();
    SM_USERS CreateUser(SM_USERS userDM);
    List<SM_USER_GROUPS> GetUserGroupList();
    SM_USER_GROUPS GetUserGroup(int groupCd);
    List<GroupsRightsDTO> GetGroupRights(int gruopId);
    SM_USER_GROUPS CreateUserGroup(SM_USER_GROUPS userDM);
    SM_USER_GROUP_RIGHTS CreateUserGroupRight(SM_USER_GROUP_RIGHTS userDM);
    bool DeleteUserGroupRights(long groupCd);
}