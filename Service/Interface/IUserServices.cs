namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetByAccout(string account);
        User GetById(int id);
        string GetRoleIdByUserId(int id);
        int GetPartitionIdsByRoleIds(int id);
        int GetDepartmentIdsByRoleIds(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}