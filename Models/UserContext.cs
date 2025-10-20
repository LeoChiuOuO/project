namespace WebApplication_Dianthus.Models;
public class UserContext
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ip { get; set; }
    public int? PartitionId { get; set; }
    public int? DepartmentId { get; set; }
}