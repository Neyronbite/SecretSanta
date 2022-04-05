using System;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Models
{
    public class ListModel
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? Time { get; set; }
    }
}