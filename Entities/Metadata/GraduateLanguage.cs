using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoekjaar.Resources;

namespace Entities
{
	[MetadataType(typeof(GraduateLanguageMetadata))]
	public partial class GraduateLanguage
	{
	}

	public class GraduateLanguageMetadata{
		public int Id { get; set; }

		public int GraduateId { get; set; }

		[Display(Name = "Language", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Language { get; set; }

		[Display(Name = "Proficiency", ResourceType = typeof(ApplicationStrings))]
		public int ProficiencyId { get; set; }
	}
}
