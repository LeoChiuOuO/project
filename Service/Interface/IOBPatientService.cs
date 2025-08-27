
// ==========================================
// 2. Service Interface and Implementation
// ==========================================

// Services/Interfaces/IOBPatientService.cs
using WebApplication_Dianthus.Models;

namespace WebApplication_Dianthus.Models.Service.Interface
{
    public interface IOBPatientService
    {
        Task<List<OBPatientRecord>> GetPatientRecordsAsync(OBPatientSearchCriteria criteria);
        Task<Dictionary<string, int>> GetStatisticsAsync(OBPatientSearchCriteria criteria);
        Task<bool> TestConnectionAsync();
        Task<List<string>> GetAvailableCompaniesAsync();
    }
}
