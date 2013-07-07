using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Zoekjaar.Web.Authentication
{
	public class WebPrincipal : IPrincipal
	{
		private IEnumerable<string> roles;

		public IIdentity Identity { get; private set; }

		public bool IsInRole(string role)
		{
			return this.roles.Any(_ => _ == role);
		}

		public WebPrincipal(IIdentity identity, IEnumerable<string> roles)
		{
			this.Identity = identity;
			this.roles = roles;
		}
	}
}