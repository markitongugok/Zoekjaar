using System;
using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;

namespace Business.Criteria
{
	public sealed class SearchCriteria
	{
		[Display(Name = "CurrentStatus", ResourceType = typeof(ApplicationStrings))]
		public int? CurrentStatusId { get; set; }
		
		[Display(Name = "Function", ResourceType = typeof(ApplicationStrings))]
		public string Function { get; set; }
		
		[Display(Name = "JobType", ResourceType = typeof(ApplicationStrings))]
		public string JobType { get; set; }

		[Display(Name = "Sector", ResourceType = typeof(ApplicationStrings))]
		public string Sector { get; set; }

		[Display(Name = "VisaStatus", ResourceType = typeof(ApplicationStrings))]
		public int? VisaStatusId { get; set; }

		[Display(Name = "DatePosted", ResourceType = typeof(ApplicationStrings))]
		public DateTime? DatePosted { get; set; }

		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int? TotalRecords { get; set; }
	}
}
