namespace Business.Criteria
{
	public sealed class UserIdentityCriteria
	{
		public string Id { get; set; }
		public string Password { get; set; }
		public bool Authenticate { get; set; }
		public int UserTypeId { get; set; }
	}
}
