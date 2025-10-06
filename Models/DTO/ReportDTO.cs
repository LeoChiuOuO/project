namespace WebApplication_Dianthus.Models.DTO;

public class ReportDTO
{
    public int Id { get; set; }
    public string SpecimenNumber { get; set; }
    public string SpecimenType { get; set; }
    public string InspectionProgress { get; set; }
    public int MrNumber { get; set; }
    public string Name { get; set; }

    public string TestItemName { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public DateTime? TestingDate { get; set; }
    public DateTime? ReportDate { get; set; }

    public string InspectionInstitution { get; set; }
    public string SendingPhysicianName { get; set; }

    public string AssessmentStatus { get; set; }
    public string NotificationStatus { get; set; }
    public string TrackingStatus { get; set; }

}
