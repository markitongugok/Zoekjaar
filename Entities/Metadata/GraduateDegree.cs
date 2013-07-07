using System.ComponentModel.DataAnnotations;

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
		
		[Required]
		public string University { get; set; }
		
		[Required]
		public string Degree { get; set; }
		
		[Required]
		public string Specialisation { get; set; }

		public virtual Graduate Graduate { get; set; }
	}
}
