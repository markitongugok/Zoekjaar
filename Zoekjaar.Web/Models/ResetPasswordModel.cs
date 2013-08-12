using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;

namespace Zoekjaar.Web.Models
{
	public sealed class ResetPasswordModel
	{
		[Required]
		[Display(Name = "NewPassword", ResourceType = typeof(ApplicationStrings))]
		public string NewPassword { get; set; }

		[Required]
		[Compare("NewPassword")]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(ApplicationStrings))]
		public string ConfirmNewPassword { get; set; }

		public bool? IsSuccessful { get; set; }

		public string Message { get; set; }

		public int UserId { get; set; }

		public int UserTypeId { get; set; }

		public bool IsTokenValid { get; set; }
	}
}