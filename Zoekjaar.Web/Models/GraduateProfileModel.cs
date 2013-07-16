using System.Collections.Generic;
using System.Web.Mvc;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class GraduateProfileModel
	{
		public Graduate Graduate { get; set; }

		public IEnumerable<Entities.Identifiers.CurrentStatus> CurrentStatus { get; set; }

		public IEnumerable<Entities.Identifiers.VisaStatus> VisaStatus { get; set; }

		public IEnumerable<Entities.Identifiers.Proficiency> Proficiencies { get; set; }

		public GraduateDegree Degree { get; set; }

		public GraduateLanguage Language { get; set; }

		public GraduateExperience Experience { get; set; }

		public bool? IsSuccessful { get; set; }
	}
}