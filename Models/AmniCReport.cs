namespace WebApplication_Dianthus.Models;
public class AmniCReport : BaseReport
{
    public string SpecimenType { get; set; }
    public string ReferralToInstitutions { get; set; }
    public string WrittenReportProcessingMethood { get; set; }
    public DateTime ChrReportDate { get; set; }
    public string ChrReportResults { get; set; }
    public DateTime WaferReportDate { get; set; }
    public string WaferReportResults { get; set; }
    public string V2V3TestingResults { get; set; }
    public DateTime GeneReportDate { get; set; }
    public string GeneReportResults { get; set; }
    public DateTime OtherReportDate { get; set; }
    public string OtherReportResults { get; set; }
}
