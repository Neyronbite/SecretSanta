using SecretSanta.BLL.Interfaces;
using SecretSanta.DAL.Interfaces;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Services
{
    public class ListActionsService : IListActionsService
    {
        private IUserService _userService;
        private IOwnerService _ownerService;
        private IListService _listService;
        private IUnitOfWork _unitOfWork;

        public ListActionsService(IUserService userService, IOwnerService ownerService, IListService listService, IUnitOfWork unitOfWork)
        {
            _listService = listService;
            _ownerService = ownerService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public string Create(ListActionsModel listActionsModel)
        {
            string error = CheckValidation(listActionsModel);
            if (error != null)
            {
                return error;
            }

            if (_listService.Create(listActionsModel.List, true))
            {
                int listID = _listService.GetListID(listActionsModel.List);

                foreach (var user in listActionsModel.Users)
                {
                    user.ListID = listID;
                }

                if (_userService.Create(listActionsModel.Users, true))
                {
                    return null;
                }
            }
            return "Something went wrong";
        }

        public string Delete(int listID)
        {
            if (!_userService.Delete(listID, true))
            {
                return "can't delete users";
            }
            if (!_listService.Delete(listID, true))
            {
                return "can't delete list";
            }

            return null;
        }

        public string Update(ListActionsModel listActionsModel)
        {
            string listOldName = _listService.GetByID(listActionsModel.List.ID).Name;
            string error = CheckValidation(listActionsModel, listOldName);

            if (error != null)
            {
                return error;
            }
            //Some validations for more confidence
            int listID = _listService.GetListID(listActionsModel.List);

            foreach (var user in listActionsModel.Users)
            {
                user.ListID = listID;
            }

            if (_listService.Update(listActionsModel.List, true) && _userService.Update(listActionsModel.Users, true))
            {
                return null;
            }
            return "Something went wrong";
        }

        private string CheckValidation(ListActionsModel listActionsModel)
        {
            string error;
            if (_ownerService.HasSameList(listActionsModel.List))
            {
                error = "You have list with same name";
            }

            else if (_userService.ContainsSameUsers(listActionsModel.Users))
            {
                error = "There are same users";
            }
            else
            {
                error = null;
            }

            return error;
        }
        private string CheckValidation(ListActionsModel listActionsModel, string listOldName)
        {
            string error;
            if (listActionsModel.List.Name == listOldName)
            {
                error = null;
            }
            else if (_ownerService.HasSameList(listActionsModel.List))
            {
                error = "You have list with same name";
            }

            if (_userService.ContainsSameUsers(listActionsModel.Users))
            {
                error = "There are same users";
            }
            else
            {
                error = null;
            }

            return error;
        }
    }
}
