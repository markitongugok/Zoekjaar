using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;

namespace Zoekjaar.Web.Models
{
	public sealed class HomeModel
	{
		public IEnumerable<JobView> FeaturedJobs { get; set; }

		public IEnumerable<JobView> LatestJobs { get; set; }

		public IEnumerable<JobView> FeaturedInternships { get; set; }

		public IEnumerable<CompanyView> FeaturedCompanies { get; set; }
	}
}