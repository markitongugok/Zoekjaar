using Entities;

namespace Business
{
	public sealed class CompanyRepository : RepositoryBase<Company>
	{
		public override Company Create()
		{
			var company = base.Create();
			company.User = this.Context.Users.Create();

			return company;
		}

		public override void Add(Company entity)
		{
			entity.User.IsActive = true;
			entity.User.UserType = 2;
			entity.IsActive = true;
			base.Add(entity);
		}
	}
}
