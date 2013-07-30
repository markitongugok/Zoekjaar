using System.Collections.Generic;
using System.Linq;
using Business.Criteria;
using Entities;

namespace Business
{
	public sealed class CandidateViewRepository : SearchRepositoryBase<CandidateView, CandidateSearchCriteria>
	{
		public override IEnumerable<CandidateView> Fetch(CandidateSearchCriteria criteria)
		{
			return this.Context.Jobs
				.Where(_ => _.Id == criteria.JobId && _.CompanyId == criteria.CompanyId)
				.SelectMany(_ => _.JobApplications)
				.Select(_ => new CandidateView
				{
					GraduateId = _.GraduateId,
					Name = _.Graduate.LastName + ", " + _.Graduate.FirstName,
					RecruitmentStatus = (Identifiers.RecruitmentStage)_.StatusId,
				});
		}
	}
}
