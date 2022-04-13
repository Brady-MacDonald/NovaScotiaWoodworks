using System.ComponentModel.DataAnnotations;

namespace NovaScotiaWoodworks.Models
{
    /// <summary>
    /// Stores information on each users account
    /// </summary>
    public class UserModel
    {
        [Key]
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
