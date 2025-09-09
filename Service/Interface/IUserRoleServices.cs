namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IUserRoleService
    {
        IEnumerable<UserRole> GetAll();
        UserRole GetByUserId(int id);
        UserRole GetByRoleId(int id);
        void CreateUserRole(UserRole userRole);
        void UpdateUserRole(UserRole userRole);
        void DeleteUserRole(int id);
    }
}