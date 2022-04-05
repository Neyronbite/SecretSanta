using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SecretSanta.BLL;
using SecretSanta.BLL.Interfaces;
using SecretSanta.BLL.Services;
using SecretSanta.FilterAttributes;
using SecretSanta.Models;
using SimpleInjector;

namespace SecretSanta.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private IOwnerService _ownerService { get; set; }
        private IListService _listService { get; set; }
        private IUserService _userService { get; set; }

        public HomeController(/*IOwnerService ownerService, IListService listService, IUserService userService*/ Container container)
        {
            _ownerService = /*ownerService*/container.GetInstance<IOwnerService>();
            _listService = /*listService*/ container.GetInstance<IListService>();
            _userService = /*userService*/ container.GetInstance<IUserService>();
        }

        [AllowAnonymous]
        [RestoreModelStateFromTempData]
        public ActionResult Index(IndexModel model)
        {
            model.LoginModel = (LoginModel)TempData["loginModel"];
            model.RegisterModel = (RegisterModel)TempData["registerModel"];

            if (User.Identity.IsAuthenticated)
            {
                //TODO don't show expired lists
                var owner = _ownerService.Get(User.Identity.Name);
                var lists = _listService.Get(owner.ID);

                model = new IndexModel(owner, lists);
            }
            else if (model.LoginModel == null && model.RegisterModel == null)
            {
                model = new IndexModel();
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult _Login()
        {
            var model = new LoginModel();
            return PartialView(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [SetTempDataModelState]
        public ActionResult _Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["loginModel"] = model;
                return RedirectToAction("Index");
            }

            if (_ownerService.Login(model.Owner, model.RememberMe))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("loginError", "Wrong Password or Login");
                TempData["loginModel"] = model;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult _Register()
        {
            var model = new RegisterModel();
            return PartialView(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [SetTempDataModelState]
        public ActionResult _Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["registerModel"] = model;
                return RedirectToAction("Index");
            }

            if (!_ownerService.CheckOwner(model.Owner))
            {
                ModelState.AddModelError("registerError", "There is a user with the same name");
                TempData["registerModel"] = model;
                return RedirectToAction("Index");
            }

            if (_ownerService.Register(model.Owner))
            {
                _ownerService.Login(model.Owner, true);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("registerError", "Wrong Password or Login");
                TempData["registerModel"] = model;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}