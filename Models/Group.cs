using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;
[Table("group")]
public class Group
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(20)]
    public string Name { get; set; }

    [Column("department_id")]
    public int DepartmentId { get; set; }

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

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
}