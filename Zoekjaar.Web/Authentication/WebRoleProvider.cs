using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using Business;

namespace Zoekjaar.Web.Authentication
{
	public sealed class WebRoleProvider : RoleProvider
	{
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName { get; set; }

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] GetRolesForUser(string username)
		{
			var principal = HttpContext.Current.User;

			if (principal == null || !principal.Identity.IsAuthenticated)
			{
				return new string[0];
			}

			var user = principal.Identity as UserIdentity;

			if (user == null || user.Name != username)
			{
				throw new InvalidOperationException("Unable to return roles for other than the current user");
			}

			return user.Roles.ToArray();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			var principal = HttpContext.Current.User;
			if (principal == null || !principal.Identity.IsAuthenticated)
			{
				return false;
			}
			if (principal.Identity == null || principal.Identity.Name != username)
			{
				throw new InvalidOperationException("Unable to return roles for other than the current user");
			}
			return principal.IsInRole(roleName);
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}
	}
}