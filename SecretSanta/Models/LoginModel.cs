using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SecretSanta.Models
{
    public class LoginModel
    {
        public OwnerModel Owner { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}