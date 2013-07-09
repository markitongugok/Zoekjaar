using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;

namespace Entities
{
	[MetadataType(typeof(GraduateMetadata))]
	public partial class Graduate
	{
	}

	public class GraduateMetadata
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		[Display(Name = "FirstName", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string FirstName { get; set; }

		[Display(Name = "LastName", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string LastName { get; set; }

		[Display(Name = "Profile", ResourceType = typeof(ApplicationStrings))]
		public string Profile { get; set; }

		[Display(Name = "CurrentStatus", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public int? CurrentStatusId { get; set; }

		[Display(Name = "VisaStatus", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public int? VisaStatusId { get; set; }

		[Display(Name = "AvailableFromDate", ResourceType = typeof(ApplicationStrings))]
		[Required]
		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
		//[DataType(DataType.Date)]
		public DateTime? AvailableFromDate { get; set; }

		[Display(Name = "PcSkills", ResourceType = typeof(ApplicationStrings))]
		public string PcSkills { get; set; }

		[Display(Name = "OtherSkills", ResourceType = typeof(ApplicationStrings))]
		public string OtherSkills { get; set; }

		[Display(Name = "LinkedIn", ResourceType = typeof(ApplicationStrings))]
		[DataType(DataType.Url)]
		public string LinkedIn { get; set; }

		[Display(Name = "GooglePlus", ResourceType = typeof(ApplicationStrings))]
		[DataType(DataType.Url)]
		public string GooglePlus { get; set; }

		public bool IsActive { get; set; }

		public virtual Lookup CurrentStatus { get; set; }

		public virtual User User { get; set; }

		public virtual Lookup VisaStatus { get; set; }

		public virtual ICollection<GraduateDegree> GraduateDegrees { get; set; }

		public virtual ICollection<GraduateExperience> GraduateExperiences { get; set; }

		public virtual ICollection<GraduateLanguage> GraduateLanguages { get; set; }

		public virtual ICollection<JobApplication> JobApplications { get; set; }
	}
}
