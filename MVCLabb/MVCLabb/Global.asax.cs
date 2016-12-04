using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCLabb.App_Start;
using System.Web.Helpers;
using System.Security.Claims;
using Autofac;
using MVCLabb.Data.Repositories;
using MVCLabb.Data.Repositories.Interfaces;
using Autofac.Integration.Mvc;

namespace MVCLabb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetupContainer();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); // Säger till att filters skall användas när appen körs.
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;
        }

        public void SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder
                .RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<GalleryRepository>()
                .As<IGalleryRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<PictureRepository>()
                .As<IPictureRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CommentRepository>()
                .As<ICommentRepository>()
                .InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(
                new AutofacDependencyResolver(container));

        }
    }
}
