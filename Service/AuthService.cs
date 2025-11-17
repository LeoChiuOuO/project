using System.Data;
using Dapper;
using Org.BouncyCastle.Crypto.Utilities;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;
using ZstdSharp.Unsafe;

public class AuthService : IAuthService, IUserContextService

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
        var account = _http.HttpContext?.Session.GetString("Account");
        if (string.IsNullOrEmpty(account)) throw new UnauthorizedAccessException("未登入");

        var sql = @"
            SELECT
                u.id AS UserId, u.account,
                r.id AS RoleId, r.name AS RoleName,
                rp.id AS RolePermissionId, rp.partition_id, rp.department_id, rp.permissions_id,
                p.id AS PermissionId, p.name AS PermissionName,
                p.review_permissions AS ReviewPermissions,
                p.create_permissions AS CreatePermissions,
                p.edit_permissions AS EditPermissions,
                p.dele_permissions AS DeletePermissions
            FROM users u
            JOIN role_user ru ON ru.user_id = u.id
            JOIN roles r ON r.id = ru.role_id
            JOIN role_permissions rp ON rp.role_id = r.id
            JOIN permissions p ON p.id = rp.permissions_id
            WHERE u.account = @Account AND u.deleted_at IS NULL;
        ";

        var lookup = new Dictionary<int, User>();

        _db.Query<User, Role, RolePermission, Permission, User>(
            sql,
            (user, role, rp, permission) =>
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

                if (rp != null)
                {
                    // rp.Permission 由 Dapper 映射的 permission 填入
                    rp.Permission = permission;
                    existingRole.RolePermissions.Add(rp);
                }

                return foundUser;
            },
            new { Account = account },
            splitOn: "RoleId,RolePermissionId,PermissionId"
        );

        return lookup.Values.FirstOrDefault() ?? throw new UnauthorizedAccessException("找不到使用者");
    }

    public bool IsAdmin(int userId)
    {
        var user = _userRepository.GetById(userId);
        var isAdmin = false;
        if (user.Account == "admin")isAdmin = true;
        if(user.Account == "alice")isAdmin = true;
        return user != null && isAdmin;
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

    public UserContext GetUserContext()
    {
        var user = GetCurrentUser();
        var ip = _http.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "0.0.0.0";
        var (partitionId, departmentId) = GetDataScope("Report");

        return new UserContext
        {
            Id = user.Id,
            Name = user.Name,
            Ip = ip,
            PartitionId = partitionId,
            DepartmentId = departmentId
        };
    }

    public int GetUserId() => GetCurrentUser().Id;

    public int? GetPartitionId() => GetUserContext().PartitionId;

    public int? GetDepartmentId() => GetUserContext().DepartmentId;

    public string GetUserName() => GetCurrentUser().Name;
}