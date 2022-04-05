using Newtonsoft.Json;
using SecretSanta.BLL;
using SecretSanta.Configuration;
using SecretSanta.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SecretSanta
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private OwnerModel ownerData;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SimpleInjectorConfiguration.Register(GlobalConfiguration.Configuration);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
                return;

            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }

            // retrieve UserData
            ownerData = JsonConvert.DeserializeObject<OwnerModel>(authTicket.UserData);

            if (Context.User != null)
                Context.User = new GenericPrincipal(Context.User.Identity, null);
        }

        protected void Application_PostAuthenticateRequest()
        {
            if (Request.IsAuthenticated)
            {
                ClaimsIdentity identity = ClaimsPrincipal.Current.Identities.First();
                identity.AddClaims(new List<Claim>
                {
                    new Claim("ID", ownerData.ID.ToString()),
                    //new Claim("Password", ownerData.Password.ToString()),
                    new Claim("Name", ownerData.Name.ToString())
                });
            }
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            //coding in here
        }
    }
}
