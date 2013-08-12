namespace Zoekjaar.Web.Models
{
	public sealed class AccountActivationModel
	{
		public int UserId { get; set; }
		public int UserTypeId { get; set; }
		public bool IsSuccessful { get; set; }
		public string Message { get; set; }
	}
}