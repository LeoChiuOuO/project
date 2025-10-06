using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;

[Table("department")]
public class Department
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(20)]
    public string Name { get; set; }

    [Column("partition_id")]
    public int PartitionId { get; set; }

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

    [ForeignKey("PartitionId")]
    public Partition Partition { get; set; }
    
    public ICollection<Report> Reports { get; set; }
}