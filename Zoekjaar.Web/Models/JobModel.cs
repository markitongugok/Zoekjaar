using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class JobModel
	{
		public Job Job { get; set; }

		public bool? IsSuccessful { get; set; }

		public IEnumerable<Entities.Identifiers.VisaStatus> VisaStatus { get; set; }

		public IEnumerable<Entities.Identifiers.JobType> JobTypes { get; set; }
	}
}