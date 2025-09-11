using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;

public class UnifiedReportViewModel : BaseReport
{
    public string SourceTable { get; set; } // amni_c, fxs, sma, carrier_scan, nips, gene_chromosome_pcb

    // 特有欄位可選
    [Column("specimen_type")]
    public string SpecimenType { get; set; } //Confirm檢體類別

    [Column("referral_to_institutions")]
    public string ReferralToInstitutions { get; set; } //轉介院所

    [Column("written_report_processing_methood")]
    public string WrittenReportProcessingMethood { get; set; } //書面報告處理方式

    [Column("chr_report_date")]
    public DateTime ChrReportDate { get; set; } //染色體報告日期

    [Column("chr_report_results")]
    public string ChrReportResults { get; set; } //染色體報告結果

    [Column("wafer_report_date")]
    public DateTime WaferReportDate { get; set; } //晶片日期

    [Column("wafer_report_results")]
    public string WaferReportResults { get; set; } //晶片結果

    [Column("v2_v3_testing_results")]
    public string V2V3TestingResults { get; set; } //V2.0+V3.0基因結果

    [Column("gene_report_date")]
    public string GeneReportDate { get; set; } //基因檢測報告日期

    [Column("gene_report_results")]
    public string GeneReportResults { get; set; } //基因檢測報告結果

    [Column("other_report_date")]
    public string OtherReportDate { get; set; } //其他檢測報告日期

    [Column("other_report_results")]
    public string OtherReportResults { get; set; } //其他檢測報告結果

    [Column("fmr1_report_results")]
    public string Fmr1ReportResults { get; set; } //FMR1結果

    [Column("report_results")]
    public string ReportResults { get; set; } //報告結果

}