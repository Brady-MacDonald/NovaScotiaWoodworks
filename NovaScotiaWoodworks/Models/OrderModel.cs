using System;
using System.ComponentModel.DataAnnotations;

namespace NovaScotiaWoodworks.Models
{
    /// <summary>
    /// Contains the information on each customers order
    /// </summary>
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
        public string Finish { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

    }
}
