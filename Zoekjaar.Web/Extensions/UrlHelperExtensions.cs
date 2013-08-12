using System;
using System.Web;
using System.Web.Mvc;

namespace Zoekjaar.Web.Extensions
{
	public static class UrlHelperExtensions
	{
		public static string ContentFullPath(this UrlHelper url, string virtualPath)
		{
			var result = string.Empty;
			Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

			result = string.Format("{0}://{1}{2}",
								   requestUrl.Scheme,
								   requestUrl.Authority,
								   VirtualPathUtility.ToAbsolute(virtualPath));
			return result;
		}
	}
}