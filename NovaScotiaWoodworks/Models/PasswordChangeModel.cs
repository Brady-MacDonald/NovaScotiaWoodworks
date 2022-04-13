using System.ComponentModel.DataAnnotations;

namespace NovaScotiaWoodworks.Models
{
    public class PasswordChangeModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
