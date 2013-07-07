using System;
using System.Linq;
using System.Web;
using Business;
using Business.Core;
using Business.Criteria;
using Zoekjaar.Web.Authentication.Contracts;

namespace Zoekjaar.Web.Authentication
{
	public sealed class ApplicationAuthentication : IApplicationAuthentication
	{
		public ApplicationAuthentication(ISearchRepository<UserIdentity, UserIdentityCriteria> userRepository, IFormsAuthentication authentication)
		{
			if (userRepository == null)
			{
				throw new ArgumentNullException("userRepository");
			}

			if (authentication == null)
			{
				throw new ArgumentNullException("authentication");
			}

			this.userRepository = userRepository;
			this.authentication = authentication;
		}

		public bool Login(UserIdentityCriteria criteria)
		{
			UserIdentity identity = this.userRepository.Fetch(criteria).Single();
			if (identity.IsAuthenticated)
			{
				this.authentication.Login(identity.Name);
				HttpContext.Current.User = new WebPrincipal(identity, identity.Roles);
				return true;
			}
			return false;
		}

		public void Logout()
		{
			this.authentication.Logout();
		}

		private IFormsAuthentication authentication;
		private ISearchRepository<UserIdentity, UserIdentityCriteria> userRepository;


		public bool Load(UserIdentityCriteria criteria)
		{
			UserIdentity identity = this.userRepository.Fetch(criteria).Single();
			if (identity.IsAuthenticated)
			{
				HttpContext.Current.User = new WebPrincipal(identity, identity.Roles);
				return true;
			}
			return false;
		}
	}
}