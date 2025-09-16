using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;

[Table("permissions")]
public class Permission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("review_permissions")]
    public bool ReviewPermissions { get; set; }

    [Column("create_permissions")]
    public bool CreatePermissions { get; set; }

    [Column("edit_permissions")]
    public bool EditPermissions { get; set; }

    [Column("dele_permissions")]
    public bool DeletePermissions { get; set; }

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

    public ICollection<RolePermission> RolePermissions { get; set; }

}