namespace Zoekjaar.Web.Authentication.Contracts
{
	public interface IFormsAuthentication
	{
		void Login(string id);
		void Logout();
	}
}