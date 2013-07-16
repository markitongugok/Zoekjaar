using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Core.Extensions;

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
					StatusId = _.StatusId
				});
		}
	}
}
