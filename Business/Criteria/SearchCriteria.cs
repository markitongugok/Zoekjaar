using System;

namespace Business.Criteria
{
	public sealed class SearchCriteria
	{
		public int? CurrentStatusId { get; set; }
		public string Function { get; set; }
		public string JobType { get; set; }
		public string Sector { get; set; }
		public int? VisaStatusId { get; set; }
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int? TotalRecords { get; set; }
	}
}
