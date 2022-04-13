using System;
using System.ComponentModel.DataAnnotations;

namespace NovaScotiaWoodworks.Models
{
    public class OrderModel
    {
        [Key]
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }   
        [Required]
        public DateTime OrderTime { get; set; }

    }
}
