using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Business
{
	public sealed class JobApplicationViewRepository : SearchRepositoryBase<JobApplicationView, int>
	{
		public override IEnumerable<JobApplicationView> Fetch(int criteria)
		{
			return this.Context.JobApplications.Where(_ => _.GraduateId == criteria)
				.Select(_ => new JobApplicationView
				{
					Id = _.Id,
					GraduateId = _.GraduateId,
					JobId = _.JobId,
					Title = _.Job.Title,
					DateApplied = _.DateApplied,
					StatusId = _.StatusId,
					CompanyId = _.Job.CompanyId,
					CompanyName = _.Job.Company.Name,
					JobTypeId = (Identifiers.JobType)_.Job.JobTypeId,
					LogoUrl = _.Job.Company.LogoUrl
				});
		}
	}
}
