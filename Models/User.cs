
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("account")]
    [MaxLength(50)]
    public string Account { get; set; }

    [Required]
    [Column("password")]
    [MaxLength(255)]
    public string Password { get; set; }

    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; }

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("last_login_date")]
    public DateTime? LastLoginDate { get; set; }

    [Column("last_password_changed_date")]
    public DateTime? LastPasswordChangedDate { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
    [NotMapped]
    public ICollection<UserRole> UserRoles { get; set; }
    [NotMapped]
    public List<Role> Roles { get; set; } = new();

}