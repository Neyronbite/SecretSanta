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
    public interface IListService
    {
        List<ListModel> Get(int ownerID);
        ListModel GetByID(int listID);
        bool Create(ListModel list, bool save);
        int GetListID(ListModel list);
        bool Delete(int listID, bool save);
        bool Update(ListModel list, bool save);
    }
}
