using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;
namespace Zoekjaar.Web.Models
{
	public sealed class UserAccountModel
	{
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Username", ResourceType = typeof(ApplicationStrings))]
		[Required]
		[EmailAddress]
		public string UserName { get; set; }
		[Display(Name = "Password", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Password { get; set; }
		public int LoginType { get; set; }
		[Display(Name = "RememberMe", ResourceType = typeof(ApplicationStrings))]
		public bool RememberMe { get; set; }
	}
}