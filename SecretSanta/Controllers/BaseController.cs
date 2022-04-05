using Newtonsoft.Json;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecretSanta.Controllers
{
    public class BaseController : Controller
    {
        protected JsonNetResult JsonNet(object data)
        {
            return new JsonNetResult(data);
        }
    }
}