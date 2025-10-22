namespace WebApplication_Dianthus.Models;
public class UserRolePermissionViewModel
{
    public int UserId{ get; set; }
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string PartitionName { get; set; }
    public string DepartmentName { get; set; }
    public string PermissionName { get; set; }

    //CRUD 權限呈現用
    public bool CanCreate { get; set; }
    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
}
