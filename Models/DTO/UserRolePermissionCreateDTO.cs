namespace WebApplication_Dianthus.Models.DTO;
public class UserRolePermissionCreateDTO
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    public int PartitionId { get; set; }
    public int DepartmentId { get; set; }
    public int GroupId { get; set; }
    public int OperatorId { get; set; }
}
