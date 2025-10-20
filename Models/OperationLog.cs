namespace WebApplication_Dianthus.Models;

public class OperationLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string IpAddress { get; set; }
    public string ActionType { get; set; }
    public string Module { get; set; }
    public bool Success { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedAt { get; set; }
}