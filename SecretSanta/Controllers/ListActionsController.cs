using SecretSanta.BLL.Interfaces;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace SecretSanta.Controllers
{
    [Authorize]
    public class ListActionsController : Controller
    {
        private IOwnerService _ownerService;
        private IListService _listService;
        private IUserService _userService;

        public ListActionsController(IOwnerService ownerService, IUserService userService, IListService listService)
        {
            _ownerService = ownerService;
            _userService = userService;
            _listService = listService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var owner = _ownerService.Get(User.Identity.Name);
            var model = new ListActionsModel(owner, 3);

            model.Users[0] = (_userService.ConvertToUser(owner));
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var model = new ListActionsModel();

            model.List = _listService.GetByID(ID);
            model.Owner = _ownerService.Get(User.Identity.Name);
            model.Users = _userService.Get(ID);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Confirm(string addressorName, string name)
        {
            var user = _userService.Get(addressorName);
            var list = _listService.GetByID(user.ListID);
            var owner = _ownerService.Get(list.OwnerID);

            var model = new ConfirmModel();
            model.Owner = owner;
            model.List = list;
            model.Name = name;

            if (_userService.SetShowed(user))
            {
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }
    }
}