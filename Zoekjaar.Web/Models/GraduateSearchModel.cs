using System.Collections.Generic;
using Business;
using Business.Criteria;
using Entities;

namespace Zoekjaar.Web.Models
{
	public class GraduateSearchModel
	{
		public SearchCriteria Criteria { get; set; }
		public IEnumerable<GraduateView> Graduates { get; set; }
		public IEnumerable<Lookup> CurrentStatus { get; set; }
		public IEnumerable<Lookup> VisaStatus { get; set; }
	}
}