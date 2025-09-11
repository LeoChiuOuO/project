using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication_Dianthus.Models;

public class Report
{
    public int? Id { get; set; }
    public string ReportId { get; set; }
    public string MedicalOrder { get; set; }
    public string ConsentFormState { get; set; }
    public string SpecimenDelyState { get; set; }
    public string SendEmailState { get; set; }
    public string ProductName { get; set; }
    public string TrackingStatus { get; set; }
    public string NotificationStatus { get; set; }
    public int? PartitionId { get; set; }
    public int? DepartmentId { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public string Name { get; set; }
    public string IdNumber { get; set; }
    public int? MrNumber { get; set; }
    public string TestItem { get; set; }
    public int? Cost { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string SendingPhysicianName { get; set; }
    public string Remark { get; set; }
    public DateTime? ReportDate { get; set; }
    public string ReportResults { get; set; }
    public int? CreateId { get; set; }
    public int? ModifyId { get; set; }
    public string SpecimenNumber { get; set; }
    public DateTime TestingDate { get; set; }
    public int? WeeksOfPregnancy { get; set; }
    public DateTime? DueDate { get; set; }
    public string InspectionInstitution { get; set; }
    public int? InspectionInstitutionPhone { get; set; }
    public string ResponsibleBusinessPerson { get; set; }
    public int? ResponsibleBusinessPhone { get; set; }
    public string ResponsibleBusinessEmail { get; set; }
    public string BusinessManager { get; set; }
    public int? BusinessManagerPhone { get; set; }
    public string BusinessManagerEmail { get; set; }
    public string AbnormalReportDeliveryMethod { get; set; }
    public string AbnormalReportNotificationMethod { get; set; }
    public string InspectionGroup { get; set; }
    public string NotificationCircumstances { get; set; }
    public DateTime? PrenatalTestingProjectTrackingTime { get; set; }
    public DateTime? ConfirmSpecimenSubmissionTime { get; set; }
    public string ConfirmSpecimenType { get; set; }
    public string ConfirmTheTestReportResults { get; set; }
    public DateTime? TrackingTime { get; set; }
    public string Tracking { get; set; }
    public string TrackingResults { get; set; }
    public DateTime? TrackingTheFollowupStatusOfNIPS_Cases { get; set; }
    public string ReferralInstitution { get; set; }
    public string ReferringPhysician { get; set; }
    public string WrittenReportProcessingMethood { get; set; }
    public string Fmr1ReportResults { get; set; }
    public DateTime? ChrReportDate { get; set; }
    public string ChrReportResults { get; set; }
    public DateTime? WaferReportDate { get; set; }
    public string WaferReportResults { get; set; }
    public string V2V3TestingResults { get; set; }
    public DateTime? GeneReportDate { get; set; }
    public string GeneReportResults { get; set; }
    public DateTime? OtherReportDate { get; set; }
    public string OtherReportResults { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
