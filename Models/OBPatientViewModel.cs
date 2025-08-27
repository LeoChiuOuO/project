
// Models/OBPatientViewModel.cs
namespace WebApplication_Dianthus.Models
{
    public class OBPatientViewModel
    {
        public List<OBPatientRecord> Records { get; set; } = new();
        public OBPatientSearchCriteria SearchCriteria { get; set; } = new();
        public int TotalRecords { get; set; }
        public string? ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        
        // 統計資訊
        public Dictionary<string, int> Statistics { get; set; } = new();
    }
}