using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication_Dianthus.Models.DTO;

public class UserRolePermissionEditDTO
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    public int PartitionId { get; set; }
    public int DepartmentId { get; set; }
    public int OperatorId { get; set; }

    // SelectList 包裝
    public SelectList RoleList { get; set; }
    public SelectList PermissionList { get; set; }
    public SelectList PartitionList { get; set; }
    public SelectList DepartmentList { get; set; }

}