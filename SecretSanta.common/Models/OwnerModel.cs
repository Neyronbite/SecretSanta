using System;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Models
{
    public class OwnerModel
    {
        public int ID { get; set; }
        [Required]
        //[MaxLength(32)]
        public string Name { get; set; }
        [Required]
        //[MaxLength(32)]
        public string Password { get; set; }
    }
}