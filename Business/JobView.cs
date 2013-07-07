using System.ComponentModel.DataAnnotations;
namespace Business
{
	public sealed class JobView
	{
		public int JobId { get; set; }
		public int CompanyId { get; set; }
		[Display(Name = "Company")]
		public string CompanyName { get; set; }

		[Display(Name = "Job#")]
		public string JobNumber { get; set; }
		public string Description { get; set; }
		[Display(Name = "Job Type")]
		public string JobType { get; set; }
		public string Function { get; set; }
		[Display(Name = "Hiring Manager")]
		public string HiringManager { get; set; }
		[Display(Name = "HR Manager")]
		public string HrManager { get; set; }
		public string Sector { get; set; }
		public string City { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}")]
		[Display(Name = "Start Date")]
		public System.DateTime? StartDate { get; set; }
		[DisplayFormat(DataFormatString = "{0} Active")]
		public int CandidateCount { get; set; }

		[Display(Name = "Apply")]
		public bool CanApply { get; set; }
	}
}
