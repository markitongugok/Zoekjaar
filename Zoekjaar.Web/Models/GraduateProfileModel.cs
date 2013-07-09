using System.Collections.Generic;
using System.Web.Mvc;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class GraduateProfileModel
	{
		public Graduate Graduate { get; set; }

		public IEnumerable<Lookup> CurrentStatus { get; set; }

		public IEnumerable<Lookup> VisaStatus { get; set; }

		public IEnumerable<Lookup> Proficiencies { get; set; }

		public GraduateDegree Degree { get; set; }

		public GraduateLanguage Language { get; set; }

		public GraduateExperience Experience { get; set; }
	}
}