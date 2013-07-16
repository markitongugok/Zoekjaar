using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using Zoekjaar.Resources;

namespace Zoekjaar.Web.Models
{
	public sealed class GraduateModel
	{
		public Graduate Graduate { get; set; }

		public IEnumerable<Entities.Identifiers.CurrentStatus> CurrentStatus { get; set; }

		public IEnumerable<Entities.Identifiers.VisaStatus> VisaStatus { get; set; }

		public IEnumerable<Entities.Identifiers.Proficiency> Proficiencies { get; set; }

		[Display(Name = "Email", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Email { get; set; }

		[Display(Name = "Password", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Password { get; set; }
	}
}