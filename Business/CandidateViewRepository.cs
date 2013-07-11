using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public sealed class CandidateViewRepository : SearchRepositoryBase<CandidateView, int>
	{
		public override IEnumerable<CandidateView> Fetch(int criteria)
		{
			return this.Context.Jobs
				.Where(_ => _.Id == criteria)
				.SelectMany(_ => _.JobApplications)
				.Select(_ => new CandidateView
				{
					GraduateId = _.Id,
					Name = _.Graduate.LastName + ", " + _.Graduate.FirstName,
					RecruitmentStatusId = _.StatusId,
					RecruitmentStatus = _.Status.Name
				});
		}
	}
}
