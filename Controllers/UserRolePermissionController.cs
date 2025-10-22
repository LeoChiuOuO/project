using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers;
public class UserRolePermissionController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IPermissionService _permissionService;
    private readonly IUserRoleService _userRoleService;
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IPartitionService _partitionService;
    private readonly IDepartmentService _departmentService;

    public UserRolePermissionController(
        IUserService userService,
        IRoleService roleService,
        IPermissionService permissionService,
        IUserRoleService userRoleService,
        IRolePermissionService rolePermissionService,
        IPartitionService partitionService,
        IDepartmentService departmentService)
    {
        _userService = userService;
        _roleService = roleService;
        _permissionService = permissionService;
        _userRoleService = userRoleService;
        _rolePermissionService = rolePermissionService;
        _partitionService = partitionService;
        _departmentService = departmentService;
    }

    // 顯示所有使用者角色權限對應
    public IActionResult Index()
    {
        var users = _userService.GetAllUsers();
        var viewModel = users
        .Where(user => _userRoleService.GetRoleIdsByUserId(user.Id) != null)
        .Select(user =>
        {
            var roleId = int.Parse(_userRoleService.GetRoleIdsByUserId(user.Id));
            if (roleId == null)
            {
                return new UserRolePermissionViewModel
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    RoleName = "尚未設定",
                    PartitionName = "-",
                    DepartmentName = "-",
                    PermissionName = "-",
                    CanCreate = false,
                    CanRead = false,
                    CanUpdate = false,
                    CanDelete = false
                };
            }

            var role = _roleService.GetRole(roleId);
            var rolePermission = _rolePermissionService.GetRolePermissionByRoleId(roleId);
            var permission = rolePermission?.Permission;

            return new UserRolePermissionViewModel
            {
                UserId = user.Id,
                UserName = user.Name,
                RoleName = role?.Name ?? "未設定",
                PartitionName = rolePermission?.Partition?.Name ?? "未設定",
                DepartmentName = rolePermission?.Department?.Name ?? "未設定",
                PermissionName = permission?.Name ?? "未設定",
                CanCreate = permission?.CreatePermissions ?? false,
                CanRead = permission?.ReviewPermissions ?? false,
                CanUpdate = permission?.EditPermissions ?? false,
                CanDelete = permission?.DeletePermissions ?? false
            };
        }).ToList();


        return View(viewModel);
    }

    //新增對應頁面
    public IActionResult Create()
    {
        ViewBag.Users = _userService.GetAllUsers();
        ViewBag.Roles = _roleService.GetAllRoles();
        ViewBag.Permissions = _permissionService.GetAllPermissions();
        ViewBag.Partitions = _partitionService.GetAll();
        return View();
    }

    //AJAX 提交新增對應
    [HttpPost]
    public IActionResult CreateAjax(UserRolePermissionCreateDTO dto)
    {
        try
        {
            _userRoleService.CreateUserRole(new UserRole
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId
            });

            _rolePermissionService.CreateRolePermission(new RolePermission
            {
                RoleId = dto.RoleId,
                PermissionsId = dto.PermissionId,
                PartitionId = dto.PartitionId,
                DepartmentId = dto.DepartmentId,
                GroupId = dto.GroupId,
                CreateId = dto.OperatorId
            });

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            // 可加上 LogService 記錄錯誤
            return Json(new { success = false, message = ex.Message });
        }
    }

    public IActionResult Edit(int userId)
    {
        var roleId = int.Parse(_userRoleService.GetRoleIdsByUserId(userId));
        var rolePermission = _rolePermissionService.GetRolePermissionByRoleId(roleId);

        var dto = new UserRolePermissionEditDTO
        {
            UserId = userId,
            RoleId = roleId,
            PermissionId = rolePermission?.PermissionsId ?? 0,
            PartitionId = rolePermission?.PartitionId ?? 0,
            DepartmentId = rolePermission?.DepartmentId ?? 0,
            // 包裝 SelectList，供 Razor 使用 asp-items
            RoleList = new SelectList(_roleService.GetAllRoles(), "Id", "Name", roleId),
            PermissionList = new SelectList(_permissionService.GetAllPermissions(), "Id", "Name", rolePermission?.PermissionsId),
            PartitionList = new SelectList(_partitionService.GetAll(), "Id", "Name", rolePermission?.PartitionId),
            DepartmentList = new SelectList(
                _departmentService.GetByPartitionId(rolePermission?.PartitionId ?? 0),
                "Id", "Name", rolePermission?.DepartmentId
            )

        };
        return View(dto);
    }

    [HttpPost]
    public IActionResult EditAjax(UserRolePermissionEditDTO dto)
    {
        try
        {
            _userRoleService.UpdateUserRole(dto.UserId, dto.RoleId);
            _rolePermissionService.UpdateRolePermission(dto.RoleId, dto.PermissionId, dto.PartitionId, dto.DepartmentId, dto.OperatorId);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Delete(int userId)
    {
        try
        {
            _userRoleService.DeleteUserRoleWithCascade(userId);
            TempData["Success"] = "刪除成功";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"刪除失敗：{ex.Message}";
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult GetPermissionCrud(int id)
    {
        var permission = _permissionService.GetPermission(id);
        if (permission == null)
            return Json(new { success = false });

        return Json(new
        {
            success = true,
            create = permission.CreatePermissions,
            read = permission.ReviewPermissions,
            update = permission.EditPermissions,
            delete = permission.DeletePermissions
        });
    }

    [HttpGet]
    public IActionResult GetDepartmentsByPartition(int partitionId)
    {
        var departments = _departmentService.GetByPartitionId(partitionId)
            .Select(d => new { id = d.Id, name = d.Name })
            .ToList();

        return Json(departments);
    }
}