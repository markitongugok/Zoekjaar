using Autofac;
using Autofac.Integration.Mvc;
using Business.Core;
using Zoekjaar.Web.Authentication;
using Zoekjaar.Web.Authentication.Contracts;

namespace Zoekjaar.Web
{
	public class WebContainerComposer : IComposer<ContainerBuilder>
	{
		public void Compose(ContainerBuilder component)
		{
			var assembly = typeof(WebContainerComposer).Assembly;
			component.RegisterControllers(assembly).PropertiesAutowired();
			component.RegisterModelBinders();
			component.RegisterModelBinderProvider();
			component.RegisterFilterProvider();
			component.RegisterType<WebFormsAuthentication>().As<IFormsAuthentication>();
			component.RegisterType<ApplicationAuthentication>().As<IApplicationAuthentication>();
		}
	}
}