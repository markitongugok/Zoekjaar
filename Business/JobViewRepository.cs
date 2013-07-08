using System.Linq;
using Business.Criteria;

namespace Business
{
	public sealed class JobViewRepository : SearchRepositoryBase<JobView, SearchCriteria>
	{
		public override System.Collections.Generic.IEnumerable<JobView> Fetch(SearchCriteria criteria)
		{
			var query = this.Context.CompanyJobs.Where(_ => true);
			if (criteria.VisaStatusId.HasValue)
			{
				query = query.Where(_ => _.VisaStatusId == criteria.VisaStatusId);
			}

			if (!string.IsNullOrEmpty(criteria.JobType))
			{
				query = query.Where(_ => _.JobType == criteria.JobType);
			}

			if (!string.IsNullOrEmpty(criteria.Function))
			{
				query = query.Where(_ => _.JobFunction == criteria.Function);
			}

			if (!string.IsNullOrEmpty(criteria.Sector))
			{
				query = query.Join(this.Context.Companies.Where(_ => _.Sector == criteria.Sector),
					_ => _.CompanyId,
					_ => _.Id,
					(_, __) => _);
			}

			criteria.TotalRecords = query.Count();
			return query.OrderBy(_ => _.JobNumber)
				.Skip(criteria.PageNumber * criteria.PageSize)
				.Take(criteria.PageSize)
				.GroupJoin(this.Context.JobApplications,
				_ => _.Id,
				_ => _.JobId,
				(_, __) => new { Job = _, Applicants = __ })
				.AsEnumerable()
				.Select(_ => new JobView
				{
					JobId = _.Job.Id,
					CompanyId = _.Job.CompanyId,
					CompanyName = _.Job.Company.Name,
					Description = _.Job.JobDescription,
					JobNumber = _.Job.JobNumber,
					JobType = _.Job.JobType,
					Function = _.Job.JobFunction,
					HiringManager = _.Job.HiringManager,
					HrManager = _.Job.HrManager,
					Sector = _.Job.Company.Sector,
					City = _.Job.Company.City,
					StartDate = _.Job.StartDate,
					CandidateCount = _.Applicants.Count(),
					CanApply = !_.Applicants.Any(__ => __.GraduateId == criteria.EntityId)
				});
		}

		public override JobView Get(int id)
		{
			return this.Context.CompanyJobs.Where(_ => _.Id == id)
				.Select(_ => new JobView
				{
					JobId = _.Id,
					CompanyId = _.CompanyId,
					CompanyName = _.Company.Name,
					Description = _.JobDescription,
					JobNumber = _.JobNumber,
					JobType = _.JobType,
					Function = _.JobFunction,
					HiringManager = _.HiringManager,
					HrManager = _.HrManager,
					Sector = _.Company.Sector,
					City = _.Company.City,
					StartDate = _.StartDate,
				}).Single();
		}
	}
}
