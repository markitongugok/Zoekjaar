using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Business.Entities.Validation;
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

		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email", ResourceType = typeof(ApplicationStrings))]
		[Required]
		[UniqueUser]
		public string Email { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Name = "ConfirmEmail", ResourceType = typeof(ApplicationStrings))]
		[Compare("Email")]
		[Required]
		public string ConfirmEmail { get; set; }

		[Display(Name = "Password", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Password { get; set; }

		[Display(Name = "ConfirmPassword", ResourceType = typeof(ApplicationStrings))]
		[Compare("Password")]
		[Required]
		public string ConfirmPassword { get; set; }
	}
}