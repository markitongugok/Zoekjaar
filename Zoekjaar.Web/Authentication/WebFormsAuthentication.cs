using System.Web.Security;
using Zoekjaar.Web.Authentication.Contracts;

namespace Zoekjaar.Web.Authentication
{
	public sealed class WebFormsAuthentication : IFormsAuthentication
	{
		public void Login(string id)
		{
			FormsAuthentication.SetAuthCookie(id, false);
		}

		public void Logout()
		{
			FormsAuthentication.SignOut();
		}
	}
}