using Business.Criteria;
namespace Zoekjaar.Web.Authentication.Contracts
{
	public interface IApplicationAuthentication
	{
		bool Login(UserIdentityCriteria criteria);
		void Logout();
		bool Load(UserIdentityCriteria criteria);
	}
}
