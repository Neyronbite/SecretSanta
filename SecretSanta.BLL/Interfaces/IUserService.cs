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
    public interface IUserService
    {
        List<UserModel> Get(int listID);
        UserModel Get(string name);
        UserModel ConvertToUser(OwnerModel owner);
        bool ContainsSameUsers(IEnumerable<UserModel> users);
        bool Create(List<UserModel> users, bool save);
        bool Delete(int listID, bool save);
        bool Update(List<UserModel> users, bool save);
        bool SetShowed(UserModel user);
    }
}
