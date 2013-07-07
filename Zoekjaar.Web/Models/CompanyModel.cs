using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class CompanyModel
	{
		public Company Company { get; set; }

		public CompanyJob Job { get; set; }

		[DataType(DataType.EmailAddress)]
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public IEnumerable<Lookup> VisaStatus { get; set; }

	}
}