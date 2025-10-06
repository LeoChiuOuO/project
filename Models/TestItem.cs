using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication_Dianthus.Models;

[Table("test_item")]
public class TestItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string? name { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    // ✅ 反向導覽（可選）
    public ICollection<Report> Reports { get; set; }

}