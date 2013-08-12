using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;

namespace Zoekjaar.Web.Models
{
	public sealed class ForgotPasswordModel
	{
		[Required]
		[Display(Name = "Email", ResourceType = typeof(ApplicationStrings))]
		public string Email { get; set; }

		public bool? IsSuccessful { get; set; }

		public string Message { get; set; }
	}
}