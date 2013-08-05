using System.Collections.Generic;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class GraduateProfileModel
	{
		public Graduate Graduate { get; set; }

		public IEnumerable<Entities.Identifiers.CurrentStatus> CurrentStatus { get; set; }

		public IEnumerable<Entities.Identifiers.VisaStatus> VisaStatus { get; set; }

		public IEnumerable<Entities.Identifiers.Proficiency> Proficiencies { get; set; }

		public GraduateDegree DegreeTemplate { get; set; }

		public GraduateLanguage LanguageTemplate { get; set; }

		public GraduateExperience ExperienceTemplate { get; set; }

		public bool? IsSuccessful { get; set; }

	}
}