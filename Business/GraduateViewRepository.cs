using System;
using System.Collections.Generic;
using System.Linq;
using Business.Criteria;
using Core.Extensions;
using Entities;

namespace Business
{
	public sealed class GraduateViewRepository : SearchRepositoryBase<GraduateView, GraduateSearchCriteria>
	{
		public override IEnumerable<GraduateView> Fetch(GraduateSearchCriteria criteria)
		{
			var query = this.Context.Graduates.Where(_ => true);
			if (criteria.CurrentStatusId.HasValue)
			{
				query = query.Where(_ => _.CurrentStatusId == criteria.CurrentStatusId);
			}
			if (criteria.VisaStatusId.HasValue)
			{
				query = query.Where(_ => _.VisaStatusId == criteria.VisaStatusId);
			}

			criteria.TotalRecords = query.Count();
			return query.OrderBy(_ => _.LastName)
				.ThenBy(_ => _.FirstName)
				.Skip(criteria.PageNumber * criteria.PageSize)
				.Take(criteria.PageSize).AsEnumerable()
				.Select(_ => new GraduateView
				{
					GraduateId = _.Id,
					Name = string.Format("{0}, {1}", _.LastName, _.FirstName),
					VisaStatus = ((Identifiers.VisaStatus)_.VisaStatusId).GetDisplayName(),
					CurrentStatus = ((Identifiers.CurrentStatus)_.CurrentStatusId).GetDisplayName(),
					Applications = _.JobApplications.Where(__ => __.Job.CompanyId == criteria.EntityId).Select(__ => new Tuple<int, string>(__.JobId, __.Job.Title))
				});
		}
	}
}
