using System.Collections.Generic;
using Business;
using Business.Criteria;
using Entities;

namespace Zoekjaar.Web.Models
{
	public class GraduateSearchModel
	{
		public GraduateSearchCriteria Criteria { get; set; }
		public IEnumerable<GraduateView> Graduates { get; set; }
		public IEnumerable<Entities.Identifiers.CurrentStatus> CurrentStatus { get; set; }
		public IEnumerable<Entities.Identifiers.VisaStatus> VisaStatus { get; set; }
	}
}