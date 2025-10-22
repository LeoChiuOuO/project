namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IUserRoleService
    {
        UserRole GetByUserId(int id);
        string GetRoleIdsByUserId(int id);
        void CreateUserRole(UserRole userRole);
        void UpdateUserRole(int userId,int roleId);
        void DeleteUserRole(int id);
        void DeleteUserRoleWithCascade(int userId);
    }
}