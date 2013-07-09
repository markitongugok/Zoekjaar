using System.Web.Mvc;
using System.Web.Routing;

namespace Zoekjaar.Web
{
	public class RouteConfig
	{
		public const string Id = "id";
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}