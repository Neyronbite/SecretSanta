using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretSanta.Models
{
    public class ConfirmModel
    {
        public OwnerModel Owner { get; set; }
        public ListModel List { get; set; }
        public string Name { get; set; }
    }
}