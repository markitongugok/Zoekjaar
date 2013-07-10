using System.Collections.Generic;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class JobsModel
	{
		public IEnumerable<Job> PostedJobs { get; set; }
	}
}