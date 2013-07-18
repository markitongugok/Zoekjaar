using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoekjaar.Resources;

namespace Entities
{
	[MetadataType(typeof(GraduateExperienceMetadata))]
	public partial class GraduateExperience
	{
	}

	public class GraduateExperienceMetadata
	{
		public int Id { get; set; }

		public int GraduateId { get; set; }

		[Display(Name = "Name", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string CompanyName { get; set; }

		[Display(Name = "Profile", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string CompanyProfile { get; set; }

		[Display(Name = "Experience", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Experience { get; set; }
	}
}
