namespace WebApplication_Dianthus.Models.DTO;

public class PermissionDTO
{
    public int Id { get; set; }
    public bool ReviewPermissions { get; set; }
    public bool CreatePermissions { get; set; }
    public bool EditPermissions { get; set; }
    public bool DeletePermissions { get; set; }
}