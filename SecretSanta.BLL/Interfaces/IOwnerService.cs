using SecretSanta.Models;
using SecretSanta.DAL;
using SecretSanta.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.DAL.Interfaces;

namespace SecretSanta.BLL.Interfaces
{
    public interface IOwnerService
    {
        bool Login(OwnerModel owner, bool rememberMe);
        OwnerModel Get(string name);
        OwnerModel Get(int ID);
        bool Register(OwnerModel owner);
        bool HasSameList(ListModel list);
        string GetClaim(string Type); 
        bool CheckOwner(OwnerModel owner);
    }
}
