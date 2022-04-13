using System.ComponentModel.DataAnnotations;

namespace NovaScotiaWoodworks.Models
{
    /// <summary>
    /// Contains the information to be sent to admin email <br/>
    /// novascotiawoodworks@gmail.com
    /// </summary>
    public class ContactModel
    {
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
