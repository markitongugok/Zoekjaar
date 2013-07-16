using System;
using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;
namespace Business
{
	public sealed class JobView
	{
		public int JobId { get; set; }
		
		[Display(Name = "Company", ResourceType = typeof(ApplicationStrings))]
		public int CompanyId { get; set; }

		[Display(Name = "Title", ResourceType = typeof(ApplicationStrings))]
		public string Title { get; set; }

		[Display(Name = "Company", ResourceType = typeof(ApplicationStrings))]
		public string CompanyName { get; set; }

		[Display(Name = "JobNumber", ResourceType = typeof(ApplicationStrings))]
		public string JobNumber { get; set; }

		[Display(Name = "Description", ResourceType = typeof(ApplicationStrings))]
		public string Description { get; set; }

		[Display(Name = "JobType", ResourceType = typeof(ApplicationStrings))]
		public int JobTypeId { get; set; }

		[Display(Name = "Function", ResourceType = typeof(ApplicationStrings))]
		public string Function { get; set; }

		[Display(Name = "HiringManager", ResourceType = typeof(ApplicationStrings))]
		public string HiringManager { get; set; }

		[Display(Name = "HrManager", ResourceType = typeof(ApplicationStrings))]
		public string HrManager { get; set; }

		[Display(Name = "Sector", ResourceType = typeof(ApplicationStrings))]
		public string Sector { get; set; }

		[Display(Name = "City", ResourceType = typeof(ApplicationStrings))]
		public string City { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}")]
		[Display(Name = "StartDate", ResourceType = typeof(ApplicationStrings))]
		public System.DateTime? StartDate { get; set; }
		[DisplayFormat(DataFormatString = "{0} Active")]

		[Display(Name = "Candidates", ResourceType = typeof(ApplicationStrings))]
		public int CandidateCount { get; set; }

		public string LogoUrl { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime DatePosted { get; set; }

		[Display(Name = "Apply")]
		public bool CanApply { get; set; }

		[Display(Name = "Featured", ResourceType = typeof(ApplicationStrings))]
		public bool IsFeatured { get; set; }
	}
}
