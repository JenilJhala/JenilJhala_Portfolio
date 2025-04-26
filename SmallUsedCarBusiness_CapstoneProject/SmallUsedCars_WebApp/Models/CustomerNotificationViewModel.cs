using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.ViewModels
{
    public class CustomerNotificationViewModel
    {
        [Required]
        [Display(Name = "Customer Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
