using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;

namespace Entities
{
	[MetadataType(typeof(GraduateDegreeMetadata))]
	public partial class GraduateDegree
	{
	}

	public class GraduateDegreeMetadata
	{
		public int Id { get; set; }

		public int GraduateId { get; set; }

		[Display(Name = "University", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string University { get; set; }

		[Display(Name = "Degree", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Degree { get; set; }

		[Display(Name = "Specialisation", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Specialisation { get; set; }

		public virtual Graduate Graduate { get; set; }
	}
}
