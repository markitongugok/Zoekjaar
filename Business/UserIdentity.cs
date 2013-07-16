using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Zoekjaar.Resources;

namespace Business
{
	public sealed class UserIdentity : IIdentity
	{
		public string AuthenticationType
		{
			get { return "Forms"; }
		}

		public bool IsAuthenticated { get; private set; }

		public string Name { get; private set; }

		public IEnumerable<string> Roles { get; private set; }

		public int UserId { get; private set; }

		public int EntityId { get; private set; }

		internal UserIdentity(int userId, int entityId, string name, bool isAuthenticated, IEnumerable<string> roles)
		{
			this.UserId = userId;
			this.EntityId = entityId;
			this.Name = name;
			this.IsAuthenticated = isAuthenticated;
			this.Roles = roles;
		}

	}
}
