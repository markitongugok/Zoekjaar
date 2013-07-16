using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zoekjaar.Resources;

namespace Entities
{
	[MetadataType(typeof(JobMetadata))]
	public partial class Job
	{
	}

	public class JobMetadata
	{
		public int Id { get; set; }

		public int CompanyId { get; set; }

		[Display(Name = "JobNumber", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string JobNumber { get; set; }

		[Display(Name = "HiringManager", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string HiringManager { get; set; }

		[Display(Name = "HrManager", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string HrManager { get; set; }

		[Display(Name = "JobType", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public int JobTypeId { get; set; }

		[Display(Name = "JobFunction", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string JobFunction { get; set; }

		[Display(Name = "OrgLevel", ResourceType = typeof(ApplicationStrings))]
		public string OrgLevel { get; set; }

		[AllowHtml]
		[Display(Name = "JobDescription", ResourceType = typeof(ApplicationStrings))]
		public string JobDescription { get; set; }

		[Display(Name = "Criteria", ResourceType = typeof(ApplicationStrings))]
		public string Criteria { get; set; }

		[Display(Name = "Degree", ResourceType = typeof(ApplicationStrings))]
		public string Degree { get; set; }

		[Display(Name = "Specialisation", ResourceType = typeof(ApplicationStrings))]
		public string Specialisation { get; set; }

		[Display(Name = "OtherCriteria", ResourceType = typeof(ApplicationStrings))]
		public string OtherCriteria { get; set; }

		[Display(Name = "VisaStatus", ResourceType = typeof(ApplicationStrings))]
		public int VisaStatusId { get; set; }

		[Display(Name = "StartDate", ResourceType = typeof(ApplicationStrings))]
		[DisplayFormat(DataFormatString = "{0:d}")]
		[Required]
		public Nullable<System.DateTime> StartDate { get; set; }

		[Display(Name = "Title", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Title { get; set; }

		public bool IsFeatured { get; set; }

		[Required]
		public System.DateTime DatePosted { get; set; }

		public virtual Company Company { get; set; }
		public virtual ICollection<JobApplication> JobApplications { get; set; }
	}
}
