using System.ComponentModel.DataAnnotations;

namespace WebApplication_Dianthus.Models;
public class ReportUpdateDto
{
    public int Id { get; set; }

    [Required]
    public string NotificationStatus { get; set; }

    [Required]
    public string TrackingStatus { get; set; }

    public string NotificationCircumstances { get; set; }

    [Required]
    public DateTime? ConfirmSpecimenSubmissionTime { get; set; }

    [Required]
    public string ConfirmSpecimenType { get; set; }

    [Required]
    public string ConfirmTheTestReportResults { get; set; }

    public string ReferralInstitution { get; set; }

    public string ReferringPhysician { get; set; }

    public string Remark { get; set; }
}