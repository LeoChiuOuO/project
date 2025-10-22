using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models.Service
{
    public class userRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IRolePermissionRepository _rolePermissionRepo;

        public userRoleService(IUserRoleRepository userRoleRepo,IRolePermissionRepository rolePermissionRepo)
        {
            _userRoleRepo = userRoleRepo;
            _rolePermissionRepo = rolePermissionRepo;
        }
        public void CreateUserRole(UserRole userRole)
        {
            _userRoleRepo.AddUserRole(userRole.UserId, userRole.RoleId);
        }
        
        public void UpdateUserRole(int userId,int roleId)
        {
            _userRoleRepo.UpdateUserRoles(userId, roleId);
        }

        public void DeleteUserRole(int userId)
        {
            var roleId = _userRoleRepo.GetRoleIdsByUserId(userId);
            int roId = int.Parse(roleId);
            _userRoleRepo.RemoveUserRole(userId, roId);
        }

        public void DeleteUserRoleWithCascade(int userId)
        {
            var userRole = _userRoleRepo.GetByUserId(userId);
            if (userRole == null) throw new Exception("找不到使用者角色資料");

            var roleId = userRole.RoleId;

            _userRoleRepo.Delete(userRole);

            var otherUsers = _userRoleRepo.GetOtherUsersByRole(roleId, userId);
            if (!otherUsers.Any())
            {
                _rolePermissionRepo.DeleteByRoleId(roleId);
            }
        }


        public UserRole GetByUserId(int userId)
        {
            var roleId = _userRoleRepo.GetRoleIdsByUserId(userId);
            int roId = int.Parse(roleId);
            return new UserRole
            {
                UserId = userId,
                RoleId = roId
            };
        }

        public string GetRoleIdsByUserId(int userId)
        {
            return _userRoleRepo.GetRoleIdsByUserId(userId);
        }
    }
}