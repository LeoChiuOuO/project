using System.ComponentModel.DataAnnotations;

namespace WebApplication_Dianthus.Models
{
    public class OBPatientSearchCriteria
    {
        [Display(Name = "起始日期")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "起始日期為必填")]
        public DateTime StartDate { get; set; }

        [Display(Name = "結束日期")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "結束日期為必填")]
        public DateTime EndDate { get; set; }

        [Display(Name = "公司")]
        [Required(ErrorMessage = "請選擇公司")]
        public string Company { get; set; } = "民權";

        [Display(Name = "病歷號")]
        public string? PatientNumber { get; set; }

        [Display(Name = "姓名")]
        public string? PatientName { get; set; }
    }
}
