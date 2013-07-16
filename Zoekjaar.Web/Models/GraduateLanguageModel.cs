using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class GraduateLanguageModel : GraduateDetailModelBase<GraduateLanguage>
	{
		public IEnumerable<Entities.Identifiers.Proficiency> Proficiencies { get; set; }

		public string PCSkills { get; set; }

		public string OtherSkills { get; set; }
	}
}