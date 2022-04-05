using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta.Models
{
    public class ListActionsModel
    {
        [Required]
        public OwnerModel Owner { get; set; }
        public ListModel List { get; set; }
        [Required]
        public List<UserModel> Users { get; set; }

        public ListActionsModel()
        {

        }
        public ListActionsModel(OwnerModel owner)
        {
            Owner = owner;
        }
        public ListActionsModel(OwnerModel owner, int startUsersCount)
        {
            Owner = owner;
            Users = new List<UserModel>();
            for (int i = 0; i < startUsersCount; i++)
            {
                Users.Add(new UserModel());
            }
        }
    }
}