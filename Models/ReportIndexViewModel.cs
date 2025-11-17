using WebApplication_Dianthus.Models.DTO;

namespace WebApplication_Dianthus.Models;
public class ReportIndexViewModel
{
    public PagedResult<ReportDTO> Reports { get; set; }
    public List<TestItem> TestItems { get; set; }
    public string FilterType { get; set; }

}