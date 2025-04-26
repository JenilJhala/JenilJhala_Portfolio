using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }
    }
}
