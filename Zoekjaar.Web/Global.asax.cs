using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Autofac;
using Autofac.Integration.Mvc;
using Business;
using Business.Core;
using Business.Criteria;
using Zoekjaar.Web.Authentication.Contracts;
using Zoekjaar.Web.Binders;

namespace Zoekjaar.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			ModelBinders.Binders.DefaultBinder = new ObjectBinder();

			var builder = new ContainerBuilder();
			MvcApplication.RunComposers(builder);

			IoC.Container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));

			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			var principal = HttpContext.Current.User;
			if (principal == null || !principal.Identity.IsAuthenticated || !(principal.Identity is FormsIdentity))
			{
				return;
			}

			MvcApplication.ApplicationAuthentication.Load(new UserIdentityCriteria
			{
				Id = principal.Identity.Name,
			});

		}

		private static void RunComposers(ContainerBuilder builder)
		{
			new BusinessObjectsContainerComposer().Compose(builder);
			new WebContainerComposer().Compose(builder);
		}

		private static IApplicationAuthentication ApplicationAuthentication
		{
			get { return IoC.Container.Resolve<IApplicationAuthentication>(); }
		}
	}
}