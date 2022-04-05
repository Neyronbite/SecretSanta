using SecretSanta.BLL;
using SecretSanta.BLL.Interfaces;
using SecretSanta.BLL.Services;
using SecretSanta.DAL.Interfaces;
using SecretSanta.DAL.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace SecretSanta.Configuration
{
    public class SimpleInjectorConfiguration
    {
        public static void Register(HttpConfiguration configuration)
        {
            // Create the container as usual.
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.ResolveUnregisteredConcreteTypes = true;

            // Register your types, for instance:
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            RegisterServices(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(configuration);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterServices(Container container)
        {
            container.Register<IListService, ListService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IOwnerService, OwnerService>(Lifestyle.Scoped);
            container.Register<IListActionsService, ListActionsService>(Lifestyle.Scoped);
            container.Register<IMailService, MailService>(Lifestyle.Scoped);

            //FIX
            //var serviceTypes = AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .SelectMany(t => t.GetTypes())
            //    .Where(t => t.IsClass && t.Namespace == "Testing.Services")
            //    .ToList();

            //foreach (var serviceType in serviceTypes)
            //{
            //    var interfaceType = serviceType.GetInterface($"I{serviceType.Name}");

            //    if (interfaceType != null)
            //    {
            //        container.Register(interfaceType, serviceType, Lifestyle.Scoped);
            //    }
            //}
        }
    }
}
