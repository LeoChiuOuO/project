using Microsoft.AspNetCore.SignalR;

namespace WebApplication_Dianthus.Models;

public class Report
{
    public Guid ReportID { get; set; } //檢驗報告ID
    public int Number { get; set; } //序號
    public int TestID { get; set; } //檢體編號
    public string CustName { get; set; } //個案姓名
    public string Item { get; set; } //檢測項目
    public DateTime DetectionDate { get; set; } //採檢日期
    public DateTime ReportDate { get; set; } //報告日期
    public string DetectionDepartment { get; set; } //檢測院所
    public string DetectionDoctor { get; set; } //檢測院所
    public int Notify { get; set; } //狀態通知
    public int Track { get; set; } //追蹤狀態
    public int Edit { get; set; } //編輯
    public int Review { get; set; } //檢視
    public int Describe { get; set; } //說明建議單
    public int Document { get; set; } //文獻
    public DateTime CustBirthday { get; set; } //個案生日
    public string Gender { get; set; } //性別
    public string CustID { get; set; } //身分證號
    public string PhoneNumber { get; set; } //連絡電話
    public string Indication { get; set; } //Indication標示
    public string SpecimenCategory { get; set; } //檢體類別
    public int WeeksOfPregnancy { get; set; } //懷孕週數
    public DateTime ExpectedDateOfDelivery { get; set; } //預產期
    public string Salesperson { get; set; } //負責業務
    public string SalesPhoneNumber { get; set; } //負責業務電話
    public string SalesEmail { get; set; } //負責業務e-mail
    public string SalesManager { get; set; } //業務主管
    public string SalesManagerPhoneNumber { get; set; } //業務主管電話
    public string SalesManagerEmail { get; set; } //業務主管e-mail
    public string DetectionDepartmentPhoneNumber { get; set; } //送檢院所電話
    public string AbnormalReportDeliveryMethod { get; set; } //異常報告寄送方式
    public string AbnormalReportNotificationMethod { get; set; } //異常報告通知方式
    public string InspectionGroup { get; set; } //檢驗組別
    public string ReportResults { get; set; } //報告結果
    public string NotificationStatus { get; set; } //通知狀態
    public string NotificationSituation { get; set; } //通知情形
    public string TrackingStatus { get; set; } //追蹤狀態
    public DateTime PrenatalTestingProjectTrackingTime { get; set; } //產前檢測項目追蹤時間
    public DateTime ConfirmSpecimenSubmissionTime { get; set; } //Confirm檢體進件時間
    public string ConfirmSpecimenType { get; set; } //Confirm檢體類別
    public string ConfirmTheTestReportResults { get; set; } //Confirm檢體報告結果
    public DateTime TrackingTime { get; set; } //追蹤時間
    public DateTime TrackingStatusOfNIPSCases { get; set; } //追蹤NIPS個案後續狀況時間
    public string TrackingSituation { get; set; } //追蹤情形
    public string TrackingResults { get; set; } //追蹤結果
    public string ReferralInstitution { get; set; } //轉介院所
    public string ReferringPhysician { get; set; } //轉介醫師
    public string Remark { get; set; } //備註
}
