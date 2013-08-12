using System.Collections.Generic;
using System.Linq;
using Business.Criteria;
using Entities;

namespace Business
{
	public sealed class JobViewRepository : SearchRepositoryBase<JobView, SearchCriteria>
	{
		public override System.Collections.Generic.IEnumerable<JobView> Fetch(SearchCriteria criteria)
		{
			var query = this.Context.Jobs.AsQueryable();

			if (!string.IsNullOrEmpty(criteria.Keyword))
			{
				query = query.Where(_ => _.Title.Contains(criteria.Keyword));
			}

			if (criteria.VisaStatusId.HasValue)
			{
				query = query.Where(_ => _.VisaStatusId == criteria.VisaStatusId);
			}

			if (criteria.JobTypeId.HasValue)
			{
				query = query.Where(_ => _.JobTypeId == criteria.JobTypeId);
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
					Title = _.Job.Title,
					Description = _.Job.JobDescription,
					JobNumber = _.Job.JobNumber,
					JobTypeId = _.Job.JobTypeId,
					Function = _.Job.JobFunction,
					HiringManager = _.Job.HiringManager,
					HrManager = _.Job.HrManager,
					Sector = _.Job.Company.Sector,
					City = _.Job.Company.City,
					StartDate = _.Job.StartDate,
					CandidateCount = _.Applicants.Count(),
					CanApply = !_.Applicants.Any(__ => criteria.EntityId == null || __.GraduateId == criteria.EntityId),
					MyStatus = _.Applicants.Where(a => a.GraduateId == criteria.EntityId).Select(a => ((Identifiers.RecruitmentStage)a.StatusId)).FirstOrDefault(),
					IsFeatured = _.Job.IsFeatured,
					LogoUrl = _.Job.Company.LogoUrl,
					DatePosted = _.Job.DatePosted,
					Location = _.Job.Location
				});
		}

		public override JobView Get(int id)
		{
			return this.Context.Jobs.Where(_ => _.Id == id)
				.Select(_ => new JobView
				{
					JobId = _.Id,
					CompanyId = _.CompanyId,
					CompanyName = _.Company.Name,
					Title = _.Title,
					Description = _.JobDescription,
					JobNumber = _.JobNumber,
					JobTypeId = _.JobTypeId,
					Function = _.JobFunction,
					HiringManager = _.HiringManager,
					HrManager = _.HrManager,
					Sector = _.Company.Sector,
					City = _.Company.City,
					StartDate = _.StartDate,
					IsFeatured = _.IsFeatured,
					LogoUrl = _.Company.LogoUrl,
					DatePosted = _.DatePosted,
					Location = _.Location
				}).Single();
		}

		public IEnumerable<JobView> FetchFeaturedJobs()
		{
			return this.Context.Jobs
				.OrderByDescending(_ => _.DatePosted)
				.Where(_ => _.IsFeatured)
				.Select(_ => new JobView
				{
					JobId = _.Id,
					CompanyId = _.CompanyId,
					CompanyName = _.Company.Name,
					Title = _.Title,
					Description = _.JobDescription,
					JobNumber = _.JobNumber,
					JobTypeId = _.JobTypeId,
					Function = _.JobFunction,
					HiringManager = _.HiringManager,
					HrManager = _.HrManager,
					Sector = _.Company.Sector,
					City = _.Company.City,
					StartDate = _.StartDate,
					IsFeatured = _.IsFeatured,
					LogoUrl = _.Company.LogoUrl,
					DatePosted = _.DatePosted,
					Location = _.Location
				});
		}

		public IEnumerable<JobView> FetchLatestJobs()
		{
			return this.Context.Jobs
				.Where(_ => !_.IsFeatured)
				.OrderByDescending(_ => _.DatePosted)
				.Take(10)
				.Select(_ => new JobView
				{
					JobId = _.Id,
					CompanyId = _.CompanyId,
					CompanyName = _.Company.Name,
					Title = _.Title,
					Description = _.JobDescription,
					JobNumber = _.JobNumber,
					JobTypeId = _.JobTypeId,
					Function = _.JobFunction,
					HiringManager = _.HiringManager,
					HrManager = _.HrManager,
					Sector = _.Company.Sector,
					City = _.Company.City,
					StartDate = _.StartDate,
					IsFeatured = _.IsFeatured,
					LogoUrl = _.Company.LogoUrl,
					DatePosted = _.DatePosted,
					Location = _.Location
				});
		}

		public IEnumerable<JobView> FetchFeaturedInternships()
		{
			return this.Context.Jobs
				.Where(_ => _.JobTypeId == (int)Identifiers.JobType.Internship && _.IsFeatured)
				.OrderByDescending(_ => _.DatePosted)
				.Take(10)
				.Select(_ => new JobView
				{
					JobId = _.Id,
					CompanyId = _.CompanyId,
					CompanyName = _.Company.Name,
					Title = _.Title,
					Description = _.JobDescription,
					JobNumber = _.JobNumber,
					JobTypeId = _.JobTypeId,
					Function = _.JobFunction,
					HiringManager = _.HiringManager,
					HrManager = _.HrManager,
					Sector = _.Company.Sector,
					City = _.Company.City,
					StartDate = _.StartDate,
					IsFeatured = _.IsFeatured,
					LogoUrl = _.Company.LogoUrl,
					DatePosted = _.DatePosted,
					Location = _.Location
				});
		}
	}
}
