using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Business.Entities.Validation;
using Entities;
using Zoekjaar.Resources;

namespace Zoekjaar.Web.Models
{
	public sealed class CompanyModel
	{
		public Company Company { get; set; }

		public Job Job { get; set; }

		[Display(Name = "Email", ResourceType = typeof(ApplicationStrings))]
		[DataType(DataType.EmailAddress)]
		[Required]
		[UniqueUser]
		public string Email { get; set; }

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

		public IEnumerable<Entities.Identifiers.VisaStatus> VisaStatus { get; set; }

	}
}