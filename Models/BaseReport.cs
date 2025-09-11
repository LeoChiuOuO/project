using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Dianthus.Models;
// 檢驗報告共通欄位基底類別
public class BaseReport
{
    [Column("report_id")]
    public string ReportId { get; set; } //檢驗報告序號

    [Column("consent_form_state")]
    public string ConsentFormState { get; set; } //同意書簽核狀態


    [Column("specimen_dely_state")]
    public string SpecimenDelyState { get; set; } //檢體傳送狀態


    [Column("send_email_state")]
    public string SendEmailState { get; set; } //發信狀態

    [Column("product_name")]
    public string ProductName { get; set; } //檢驗產品名稱

    [Column("tracking_status")]
    public string TrackingStatus { get; set; } //追蹤狀態

    [Column("notification_status")]
    public string NotificationStatus { get; set; } //通知狀態

    [Column("partition_id")]
    public int PartitionId { get; set; } //公司代碼

    [Column("department_id")]
    public int DepartmentId { get; set; } //單位代碼

    [Column("submission_date")]
    public DateTime SubmissionDate { get; set; } //送檢日期

    [Column("name")]
    public string Name { get; set; } //姓名

    [Column("id_number")]
    public string IdNumber { get; set; } //身分證

    [Column("mr_number")]
    public int MrNumber { get; set; } //病歷號

    [Column("test_item")]
    public string TestItem { get; set; } //檢測項目

    [Column("cost")]
    public decimal Cost { get; set; } //費用

    [Column("return_date")]
    public DateTime ReturnDate { get; set; } //回診日期

    [Column("sending_physician_name")]
    public string SendingPhysicianName { get; set; } //送檢醫師

    [Column("remark")]
    public string Remark { get; set; } //附註

    [Column("specimen_number")]
    public string SpecimenNumber { get; set; } //檢體編號

    [Column("testing_date")]
    public DateTime TestingDate { get; set; } //採檢日期

    [Column("weeks_of_pregnancy")]
    public int WeeksOfPregnancy { get; set; } //懷孕週數

    [Column("due_date")]
    public DateTime DueDate { get; set; } //預產期

    [Column("inspection_institution")]
    public string InspectionInstitution { get; set; } //送檢院所

    [Column("responsible_business_person")]
    public string ResponsibleBusinessPerson { get; set; } //負責業務

    [Column("responsible_business_phone")]
    public string ResponsibleBusinessPhone { get; set; } //負責業務電話

    [Column("business_manager")]
    public string BusinessManager { get; set; } //業務主管

    [Column("business_manager_phone")]
    public string BusinessManagerPhone { get; set; } //業務主管電話

    [Column("inspection_institution_phone")]
    public string InspectionInstitutionPhone { get; set; } //送檢院所電話

    [Column("responsible_business_email")]
    public string ResponsibleBusinessEmail { get; set; } //負責業務Email

    [Column("business_manager_email")]
    public string BusinessManagerEmail { get; set; } //業務主管Email

    [Column("abnormal_report_delivery_method")]
    public string AbnormalReportDeliveryMethod { get; set; } //異常報告寄送方式

    [Column("abnormal_report_notification_method")]
    public string AbnormalReportNotificationMethod { get; set; } //異常報告通知方式

    [Column("inspection_group")]
    public string InspectionGroup { get; set; } //檢驗組別

    [Column("notification_circumstances")]
    public string NotificationCircumstances { get; set; } //通知情形

    [Column("prenatal_testing_project_tracking_time")]
    public DateTime PrenatalTestingProjectTrackingTime { get; set; } //產前檢測項目追蹤時間

    [Column("confirm_specimen_submission_time")]
    public DateTime ConfirmSpecimenSubmissionTime { get; set; } //Confirm檢體進件時間

    [Column("confirm_specimen_type")]
    public string ConfirmSpecimenType { get; set; } //Confirm檢體類別

    [Column("confirm_the_test_report_results")]
    public string ConfirmTheTestReportResults { get; set; } //Confirm檢體報告結果

    [Column("tracking_time")]
    public DateTime TrackingTime { get; set; } //追蹤時間

    [Column("tracking")]
    public string Tracking { get; set; } //追蹤情形
    
    [Column("tracking_the_followup_status_of_NIPS_cases")]
    public string TrackingTheFollowupStatusOfNIPS { get; set; } //追蹤NIPS個案後續狀況時間

    [Column("tracking_results")]
    public string TrackingResults { get; set; } //追蹤結果

    [Column("referral_institution")]
    public string ReferralInstitution { get; set; } //轉介院所

    [Column("referring_physician")]
    public string ReferringPhysician { get; set; } //轉介醫師

    [Column("create_id")]
    public int CreateId { get; set; } //建立者編號

    [Column("modify_id")]
    public int ModifyId { get; set; } //修改人員

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } //建立日期

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } //修改日期

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; } //刪除日期
}