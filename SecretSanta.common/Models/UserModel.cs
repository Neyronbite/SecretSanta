using System;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public int ListID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email is not vaild")]
        public string Mail { get; set; }
        public bool? Showed { get; set; }
    }
}