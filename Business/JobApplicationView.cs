using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using Zoekjaar.Resources;

namespace Business
{
	public sealed class JobApplicationView
	{
		public int Id { get; set; }
		public int GraduateId { get; set; }
		public int JobId { get; set; }
		[Display(Name = "Title", ResourceType = typeof(ApplicationStrings))]
		public string Title { get; set; }
		[Display(Name = "DateApplied", ResourceType = typeof(ApplicationStrings))]
		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
		public DateTime DateApplied { get; set; }
		[Display(Name = "RecruitmentStatus", ResourceType = typeof(ApplicationStrings))]
		public int StatusId { get; set; }
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public Identifiers.JobType JobTypeId { get; set; }
		public string LogoUrl { get; set; }
		public string Location { get; set; }
	}
}
