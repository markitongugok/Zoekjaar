using System.Collections.Generic;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class JobModel
	{
		public IEnumerable<CompanyJob> PostedJobs { get; set; }
		public CompanyJob NewJob { get; set; }
		public IEnumerable<Lookup> VisaStatus { get; set; }

		public bool? IsSuccessful { get; set; }
	}
}