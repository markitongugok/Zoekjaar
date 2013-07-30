using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;

namespace Zoekjaar.Web.Models
{
	public sealed class ChangePasswordModel
	{
		[Required]
		[Display(Name = "CurrentPassword", ResourceType = typeof(ApplicationStrings))]
		public string CurrentPassword { get; set; }

		[Required]
		[Display(Name = "NewPassword", ResourceType = typeof(ApplicationStrings))]
		public string NewPassword { get; set; }

		[Required]
		[Compare("NewPassword")]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(ApplicationStrings))]
		public string ConfirmNewPassword { get; set; }

		public bool? IsSuccessful { get; set; }
	}
}