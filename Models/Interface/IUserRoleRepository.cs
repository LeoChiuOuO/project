namespace WebApplication_Dianthus.Models.Interface;

public interface IUserRoleRepository
{
    void AddUserRole(int userId, int roleId);
    void UpdateUserRoles(int userId, int roleId);
    void RemoveUserRole(int userId, int roleId);
    string GetRoleIdsByUserId(int userId);
    UserRole GetByUserId(int userId);
    void Delete(UserRole userRole);
    IEnumerable<UserRole> GetOtherUsersByRole(int roleId, int excludeUserId);
}