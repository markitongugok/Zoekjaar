using System.Collections.Generic;
using System.Linq;
using Business.Criteria;
using Core;
using Core.Extensions;

namespace Business
{
	public class UserIdentityRepository : SearchRepositoryBase<UserIdentity, UserIdentityCriteria>
	{
		public override IEnumerable<UserIdentity> Fetch(UserIdentityCriteria criteria)
		{
			UserIdentity identity = null;
			var query = this.Context.Users.Where(_ => _.Username == criteria.Id);
			if (criteria.Authenticate)
			{
				query = query.Where(_ => _.UserType == criteria.UserTypeId);
			}
			var user = query.SingleOrDefault();
			if (user != null)
			{
				var isAuthenticated = true;
				if (criteria.Authenticate)
				{
					var generator = new PasswordGenerator(criteria.Password);
					isAuthenticated = user.Hash.IsEqualTo(generator.Password.ToArray());
				}
				else
				{
					criteria.UserTypeId = user.UserType;
				}
				identity = new UserIdentity(user.Id, criteria.UserTypeId == 1 ? user.Graduates.Single().Id : user.Companies.Single().Id, user.Username, isAuthenticated, new string[] { user.UserType == 1 ? "Graduate" : "Company" });
			}

			return new List<UserIdentity>() { identity ?? new UserIdentity(0, 0, "", false, null) };
		}
	}
}
