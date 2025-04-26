using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.Models
{
    public class PromoteEmployeeViewModel
    {
        [Required]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string CurrentPosition { get; set; }
        public bool EligibleForPromotion { get; set; }

        [Required]
        public string NewPosition { get; set; }
    }
}
