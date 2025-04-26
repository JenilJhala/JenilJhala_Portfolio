using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.Models
{
    public class RequestLeaveViewModel
    {
        [Required]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string LeaveType { get; set; }
        public string Reason { get; set; }
    }
}
