using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication_Dianthus.Models;

public class Report
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("report_id")]
    public string ReportId { get; set; }

    [Column("medical_order")]
    public string MedicalOrder { get; set; }

    [Column("consent_form_state")]
    public string ConsentFormState { get; set; }

    [Column("specimen_dely_state")]
    public string SpecimenDelyState { get; set; }

    [Column("send_email_state")]
    public string SendEmailState { get; set; }

    [Column("product_name")]
    public string ProductName { get; set; }

    [Column("tracking_status")]
    public string TrackingStatus { get; set; }

    [Column("notification_status")]
    public string NotificationStatus { get; set; }

    [Column("partition_id")]
    public int PartitionId { get; set; }

    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Column("group_id")]
    public int? GroupId { get; set; }

    [Column("submission_date")]
    public DateTime SubmissionDate { get; set; }

    [Column("received_date")]
    public DateTime ReceivedDate { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("birthday")]
    public DateTime? Birthday{get;set;}

    [Column("id_number")]
    public string IdNumber { get; set; }

    [Column("mr_number")]
    public int MrNumber { get; set; }

    [Column("test_item_id")]
    public int TestItemId { get; set; }

    [Column("cost")]
    public int Cost { get; set; }

    [Column("return_date")]
    public DateTime ReturnDate { get; set; }

    [Column("sending_physician_name")]
    public string SendingPhysicianName { get; set; }

    [Column("remark")]
    public string Remark { get; set; }

    [Column("report_date")]
    public DateTime ReportDate { get; set; }

    [Column("report_results")]
    public string ReportResults { get; set; }

    [Column("create_id")]
    public int CreateId { get; set; }

    [Column("modify_id")]
    public int ModifyId { get; set; }

    [Column("specimen_number")]
    public string SpecimenNumber { get; set; }

    [Column("specimen_type")]
    public string SpecimenType { get; set; }

    [Column("testing_date")]
    public DateTime TestingDate { get; set; }

    [Column("inspection_progress")]
    public string InspectionProgress { get; set; }

    [Column("assessment_status")]
    public string AssessmentStatus { get; set; }

    [Column("weeks_of_pregnancy")]
    public int WeeksOfPregnancy { get; set; }

    [Column("due_date")]
    public DateTime DueDate { get; set; }

    [Column("report_due_date")]
    public DateTime? ReportDueDate { get; set; }

    [Column("inspection_institution")]
    public string InspectionInstitution { get; set; }

    [Column("inspection_institution_phone")]
    public int InspectionInstitutionPhone { get; set; }

    [Column("responsible_business_person")]
    public string ResponsibleBusinessPerson { get; set; }

    [Column("responsible_business_phone")]
    public int ResponsibleBusinessPhone { get; set; }

    [Column("responsible_business_email")]
    public string ResponsibleBusinessEmail { get; set; }

    [Column("business_manager")]
    public string BusinessManager { get; set; }

    [Column("business_manager_phone")]
    public int BusinessManagerPhone { get; set; }

    [Column("business_manager_email")]
    public string BusinessManagerEmail { get; set; }

    [Column("abnormal_report_delivery_method")]
    public string AbnormalReportDeliveryMethod { get; set; }

    [Column("abnormal_report_notification_method")]
    public string AbnormalReportNotificationMethod { get; set; }

    [Column("inspection_group")]
    public string InspectionGroup { get; set; }

    [Column("notification_circumstances")]
    public string NotificationCircumstances { get; set; }

    [Column("prenatal_testing_project_tracking_time")]
    public DateTime PrenatalTestingProjectTrackingTime { get; set; }

    [Column("confirm_specimen_submission_time")]
    public DateTime ConfirmSpecimenSubmissionTime { get; set; }

    [Column("confirm_specimen_type")]
    public string ConfirmSpecimenType { get; set; }

    [Column("confirm_the_test_report_results")]
    public string ConfirmTheTestReportResults { get; set; }

    [Column("tracking_time")]
    public DateTime TrackingTime { get; set; }

    [Column("tracking")]
    public string Tracking { get; set; }

    [Column("tracking_results")]
    public string TrackingResults { get; set; }

    [Column("tracking_the_followup_status_of_NIPS_cases")]
    public DateTime TrackingTheFollowupStatusOfNIPS_Cases { get; set; }

    [Column("referral_institution")]
    public string ReferralInstitution { get; set; }

    [Column("referring_physician")]
    public string ReferringPhysician { get; set; }

    [Column("written_report_processing_methood")]
    public string WrittenReportProcessingMethood { get; set; }

    [Column("fmr1_report_results")]
    public string Fmr1ReportResults { get; set; }

    [Column("chr_report_date")]
    public DateTime ChrReportDate { get; set; }

    [Column("chr_report_results")]
    public string ChrReportResults { get; set; }

    [Column("wafer_report_date")]
    public DateTime WaferReportDate { get; set; }

    [Column("wafer_report_results")]
    public string WaferReportResults { get; set; }

    [Column("v2_v3_testing_results")]
    public string V2V3TestingResults { get; set; }

    [Column("gene_report_date")]
    public DateTime GeneReportDate { get; set; }

    [Column("gene_report_results")]
    public string GeneReportResults { get; set; }

    [Column("other_report_date")]
    public DateTime OtherReportDate { get; set; }

    [Column("other_report_results")]
    public string OtherReportResults { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    // 導覽屬性
    public TestItem TestItem { get; set; }
    public Department Department { get; set; }
    public Partition Partition { get; set; }
    public Group Group { get; set; } 

    // 一個 Report 可以有多個 ConsultRecord
    public ICollection<ConsultRecord> ConsultRecords { get; set; } = new List<ConsultRecord>();
    public ICollection<TrackingTimeline> TrackingTimelines { get; set; } = new List<TrackingTimeline>();
}
