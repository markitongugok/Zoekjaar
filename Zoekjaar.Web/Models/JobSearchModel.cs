using System.Collections.Generic;
using Business;
using Business.Criteria;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class JobSearchModel
	{
		public IEnumerable<JobView> Jobs { get; set; }
		public SearchCriteria Criteria { get; set; }
		public IEnumerable<Entities.Identifiers.VisaStatus> VisaStatus { get; set; }
		public IEnumerable<Entities.Identifiers.CurrentStatus> CurrentStatus { get; set; }
		public IEnumerable<Entities.Identifiers.JobType> JobTypes { get; set; }
	}
}