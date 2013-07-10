using System;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Zoekjaar.Web.Extensions
{
	public static class HtmlHelperExtensions
	{
		private const string ActionKey = "action";

		private const string ControllerKey = "controller";

		private const string DefaultAction = "Index";

		private const string ScriptBasePath = "~/Scripts/Views/";

		private const string ScriptFormat = "{0}.{1}.ts";

		private const string ScriptTagFormat = "<script src=\"{0}\"></script>";

		public const string StyleNameFormat = "{0}.{1}.less";

		public const string StyleBasePath = "~/Content/Views/";

		public const string StyleTagFormat = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />";

		public static MvcHtmlString ViewScript(this HtmlHelper helper)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			var routeData = helper.RouteCollection.GetRouteData(helper.ViewContext.HttpContext);
			if (routeData == null)
			{
				return null;
			}

			var action = routeData.Values.ContainsKey(HtmlHelperExtensions.ActionKey)
				? routeData.Values[HtmlHelperExtensions.ActionKey]
				: HtmlHelperExtensions.DefaultAction;

			if (!routeData.Values.ContainsKey(HtmlHelperExtensions.ControllerKey))
			{
				throw new ArgumentException("Could not find controller in route data.", "helper");
			}

			var controller = routeData.Values[HtmlHelperExtensions.ControllerKey];

			var scriptName = string.Format(CultureInfo.InvariantCulture,
				HtmlHelperExtensions.ScriptFormat,
				controller,
				action);

			var path = string.Concat(HtmlHelperExtensions.ScriptBasePath, scriptName);

			var script = string.Empty;

			if (File.Exists(helper.ViewContext.HttpContext.Server.MapPath(path)))
			{
				var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
				var scriptUrl = urlHelper.Content(path);
				script = string.Format(CultureInfo.InvariantCulture,
					HtmlHelperExtensions.ScriptTagFormat,
					scriptUrl);
			}

			return MvcHtmlString.Create(script);
		}

		public static MvcHtmlString ViewStyle(this HtmlHelper helper)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			var routeData = helper.RouteCollection.GetRouteData(helper.ViewContext.HttpContext);
			if (routeData == null)
			{
				return null;
			}

			var action = routeData.Values.ContainsKey(HtmlHelperExtensions.ActionKey)
				? routeData.Values[HtmlHelperExtensions.ActionKey]
				: HtmlHelperExtensions.DefaultAction;

			if (!routeData.Values.ContainsKey(HtmlHelperExtensions.ControllerKey))
			{
				throw new ArgumentException("Could not find controller in route data.", "helper");
			}

			var controller = routeData.Values[HtmlHelperExtensions.ControllerKey];

			var styleName = string.Format(CultureInfo.InvariantCulture,
				HtmlHelperExtensions.StyleNameFormat,
				controller,
				action);

			var path = string.Concat(HtmlHelperExtensions.StyleBasePath, styleName);

			var style = string.Empty;

			if (File.Exists(helper.ViewContext.HttpContext.Server.MapPath(path)))
			{
				var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
				var styleUrl = urlHelper.Content(path);
				style = string.Format(CultureInfo.InvariantCulture,
					HtmlHelperExtensions.StyleTagFormat,
					styleUrl);
			}

			return MvcHtmlString.Create(style);
		}

		public static MvcHtmlString ControlLabelFor<TModel, TValue>(this HtmlHelper<TModel> @this, Expression<Func<TModel, TValue>> expression)
		{
			return @this.LabelFor(expression, new { @class = "control-label" });
		}

		public static MvcHtmlString ControlValidationMessageFor<TModel, TValue>(this HtmlHelper<TModel> @this, Expression<Func<TModel, TValue>> expression)
		{
			return @this.ValidationMessageFor(expression, null, new { @class = "help-inline" });
		}

		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> @this, Expression<Func<TModel, TValue>> expression, int maxLength)
		{
			var value = expression.Compile().Invoke((TModel)@this.ViewContext.ViewData.Model) as string;

			return @this.Display(value.Length > maxLength ? value.Substring(0, maxLength) : string.Format("{0}...", value));
		}
	}
}