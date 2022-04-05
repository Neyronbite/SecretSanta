using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using SecretSanta.BLL.Interfaces;
using System.Security.Principal;

namespace SecretSanta.Controllers.API
{
    [Authorize]
    public class ListActionsAPIController : ApiController
    {
        private  IListActionsService _listActionsService;
        private IOwnerService _ownerService;
        private IListService _listService;

        public ListActionsAPIController(IListActionsService listActionsService, IOwnerService ownerService, IListService listService)
        {
            _listActionsService = listActionsService;
            _ownerService = ownerService;
            _listService = listService;
        }
        
        // POST: api/ListActionsAPI
        public IHttpActionResult Post(ListActionsModel model)
        {
            SetOwnerID(model);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("validationError", "you have invalid mail, or other invalid data");
                return BadRequest(ModelState);
            }
            //TODO add datetime error

            string error = _listActionsService.Create(model);
            if (error == null)
            {
                int id = _listService.GetListID(model.List);
                return Ok(id);
            }

            ModelState.AddModelError("validationError", error);
            return BadRequest(ModelState);
        }

        // PUT: api/ListActionsAPI/5
        public IHttpActionResult Put(ListActionsModel model)
        {
            int tempID =  model.List.OwnerID;
            SetOwnerID(model);
            if (tempID != model.Owner.ID)
            {
                ModelState.AddModelError("validationError", "you have no permissions");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("validationError", "you have invalid mail, or other invalid data");
                return BadRequest(ModelState);
            }
            //TODO add datetime error

            string error = _listActionsService.Update(model);
            if (error == null)
            {
                return Ok();
            }

            ModelState.AddModelError("validationError", error);
            return BadRequest(ModelState);
        }

        // DELETE: api/ListActionsAPI/5
        public IHttpActionResult Delete(int id)
        {
            string error = _listActionsService.Delete(id);
            if (error == null)
            {
                return Ok();
            }

            ModelState.AddModelError("deleteError", error);
            return BadRequest(ModelState);
        }

        private void SetOwnerID(ListActionsModel model)
        {
            int ownerID = int.Parse(_ownerService.GetClaim("ID"));
            model.Owner.ID = ownerID;
            model.List.OwnerID = ownerID;
        }
    }
}
