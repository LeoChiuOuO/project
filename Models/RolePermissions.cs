using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;
[Table("role_permissions")]
public class RolePermission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("partition_id")]
    public int PartitionId { get; set; }

    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("permissions_id")]
    public int PermissionsId { get; set; }

    [Column("create_id")]
    public int? CreateId { get; set; }

    [Column("modify_id")]
    public int? ModifyId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    [ForeignKey("PermissionsId")]
    public Permission Permission { get; set; }

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }

    [ForeignKey("PartitionId")]
    public Partition Partition { get; set; }

}