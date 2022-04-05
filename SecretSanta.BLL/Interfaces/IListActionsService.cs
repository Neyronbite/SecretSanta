using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace SecretSanta.BLL.Interfaces
{
    public interface IListActionsService
    {
        //returns the error message if it exists
        string Create(ListActionsModel listActionsModel);
        string Delete(int listID);
        string Update(ListActionsModel listActionsModel);
    }
}
