using AspNet.Identity.MongoDB;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MongoDB.Driver;
using SNAPME.Live.Data.Mongo;
using SNAPME.Live.Data.Repositories;
using SNAPME.Live.Models;
using SNAPME.Live.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SNAPME.Live
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            // registrating all the existing controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Init MongoDB Identity
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            var database = client.GetDatabase(ConfigurationManager.AppSettings["DBName"]);
            var usersCollection = database.GetCollection<ApplicationUser>("users");
            var rolesCollection = database.GetCollection<IdentityRole>("roles");
            
            IndexChecks.EnsureUniqueIndexOnUserName(usersCollection);
            IndexChecks.EnsureUniqueIndexOnEmail(usersCollection);
            IndexChecks.EnsureUniqueIndexOnRoleName(rolesCollection);

            builder.Register(b => database).AsSelf().SingleInstance();

            // Register Repositories
            builder.Register(b => new BetaRegistrationRepository(database)).AsImplementedInterfaces().InstancePerLifetimeScope();

            // Register Services
            builder.Register(b => new BetaRegistrationService(b.Resolve<IBetaRegistrationRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
