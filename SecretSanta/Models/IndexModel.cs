using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretSanta.Models
{
    public class IndexModel
    {
        public List<ListModel> Lists { get; set; }
        public OwnerModel Owner { get; set; }
        public LoginModel LoginModel { get; set; }
        public RegisterModel RegisterModel { get; set; }

        public IndexModel()
        {

        }
        public IndexModel(LoginModel loginModel)
        {
            LoginModel = loginModel;
        }
        public IndexModel(RegisterModel registerModel)
        {
            RegisterModel = registerModel;
        }
        public IndexModel(OwnerModel owner, List<ListModel> lists)
        {
            Owner = owner;
            Lists = lists;
        }
    }
}