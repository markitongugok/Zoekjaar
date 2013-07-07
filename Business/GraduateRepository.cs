using Entities;

namespace Business
{
	public sealed class GraduateRepository : RepositoryBase<Graduate>
	{
		public override Graduate Create()
		{
			var graduate = base.Create();
			graduate.User = this.Context.Users.Create();

			return graduate;
		}

		public override void Add(Graduate entity)
		{
			entity.User.IsActive = true;
			entity.User.UserType = 1;
			entity.IsActive = true;
			base.Add(entity);
		}

		public override Graduate Attach(Graduate entity)
		{
			this.AttachChildren<GraduateDegree>(
				entity,
				entity.GraduateDegrees,
				d => d.Id,
				(g, d) => d.GraduateId = g.Id);

			this.AttachChildren<GraduateLanguage>(
				entity,
				entity.GraduateLanguages,
				d => d.Id,
				(g, d) => d.GraduateId = g.Id);

			this.AttachChildren<GraduateExperience>(
				entity,
				entity.GraduateExperiences,
				d => d.Id,
				(g, d) => d.GraduateId = g.Id);

			return base.Attach(entity);
		}
	}
}
