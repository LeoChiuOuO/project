namespace WebApplication_Dianthus.Models.DTO;

public class EmailDTO
{
    public string Sender { get; set; }
    public List<string> Recipients { get; set; }
    public string Content { get; set; }
}