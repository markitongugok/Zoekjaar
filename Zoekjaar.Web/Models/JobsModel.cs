using System.Collections.Generic;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class JobsModel
	{
		public IEnumerable<Job> PostedJobs { get; set; }
		public Job NewJob { get; set; }
		public IEnumerable<Lookup> VisaStatus { get; set; }
		public IEnumerable<Lookup> JobTypes { get; set; }
		public bool? IsSuccessful { get; set; }
	}
}