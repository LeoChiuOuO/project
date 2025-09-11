using System.Data;
using Dapper;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;
using ZstdSharp.Unsafe;

public class AuthService : IAuthService
{
    private readonly IDbConnection _db;
    private readonly IHttpContextAccessor _http;
    private readonly IUserRepository _userRepository;
    public AuthService(IDbConnection db, IHttpContextAccessor http,IUserRepository userRepository)
    {
        _db = db;
        _http = http;
        _userRepository = userRepository;
    }

    public bool ValidateUser(string account, string password, out User user)
    {
        user = _userRepository.GetByAccount(account);
        if (user == null) return false;
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public void UpdateLastLoginDate(User user)
    {
        user.LastLoginDate = DateTime.Now;
        _userRepository.Update(user);
    }

    public User GetCurrentUser()
    {
        var account = _http.HttpContext.User.Identity?.Name;
        if (string.IsNullOrEmpty(account)) throw new UnauthorizedAccessException("未登入");

        var sql = @"
            SELECT 
                u.id AS UserId, u.account,
                r.id AS RoleId, r.name AS RoleName,
                p.id AS PermissionId, p.name AS PermissionName,
                p.review_permissions, p.create_permissions, p.edit_permissions, p.dele_permissions,
                rp.partition_id, rp.department_id
            FROM users u
            JOIN role_user ru ON ru.user_id = u.id
            JOIN roles r ON r.id = ru.role_id
            JOIN role_permissions rp ON rp.role_id = r.id
            JOIN permissions p ON p.id = rp.permissions_id
            WHERE u.account = @Account AND u.deleted_at IS NULL;
        ";

        var lookup = new Dictionary<int, User>();

        _db.Query<User, Role, RolePermission, User>(
            sql,
            (user, role, rp) =>
            {
                if (!lookup.TryGetValue(user.Id, out var foundUser))
                {
                    foundUser = user;
                    foundUser.Roles = new List<Role>();
                    lookup.Add(user.Id, foundUser);
                }

                var existingRole = foundUser.Roles.FirstOrDefault(r => r.Id == role.Id);
                if (existingRole == null)
                {
                    role.RolePermissions = new List<RolePermission>();
                    existingRole = role;
                    foundUser.Roles.Add(existingRole);
                }

                rp.Permission = new Permission
                {
                    Id = rp.PermissionsId,
                    Name = rp.Permission.Name,
                    ReviewPermissions = rp.Permission.ReviewPermissions,
                    CreatePermissions = rp.Permission.CreatePermissions,
                    EditPermissions = rp.Permission.EditPermissions,
                    DeletePermissions = rp.Permission.DeletePermissions
                };

                existingRole.RolePermissions.Add(rp);
                return foundUser;
            },
            new { Account = account },
            splitOn: "RoleId,PermissionId"
        );

        return lookup.Values.FirstOrDefault() ?? throw new UnauthorizedAccessException("找不到使用者");
    }


    public bool HasPermission(string permissionName, string action)
    {
        var user = GetCurrentUser();
        foreach (var role in user.Roles)
        {
            foreach (var perm in role.RolePermissions)
            {
                if (perm.Permission.Name == permissionName)
                {
                    return action.ToLower() switch
                    {
                        "create" => perm.Permission.CreatePermissions,
                        "read"   => perm.Permission.ReviewPermissions,
                        "update" => perm.Permission.EditPermissions,
                        "delete" => perm.Permission.DeletePermissions,
                        _ => false
                    };
                }
            }
        }

        return false;

    }

    public (int? PartitionId, int? DepartmentId) GetDataScope(string permissionName)
    {
        var user = GetCurrentUser();

        foreach (var role in user.Roles)
        {
            foreach (var perm in role.RolePermissions)
            {
                if (perm.Permission.Name == permissionName)
                {
                    return (perm.PartitionId, perm.DepartmentId);
                }
            }
        }

        return (null, null);
    }

}