using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Zoekjaar.Resources;

namespace Entities
{
	[MetadataType(typeof(CompanyMetadata))]
	public partial class Company
	{
	}

	public class CompanyMetadata
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		[Display(Name = "Name", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Name { get; set; }
		[Display(Name = "Sector", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string Sector { get; set; }
		[Display(Name = "City", ResourceType = typeof(ApplicationStrings))]
		[Required]
		public string City { get; set; }
		[AllowHtml]
		[Display(Name = "Profile", ResourceType = typeof(ApplicationStrings))]
		public string Profile { get; set; }
		[Display(Name = "Website", ResourceType = typeof(ApplicationStrings))]
		[Required]
		[DataType(DataType.Url)]
		public string Website { get; set; }
		public string LogoUrl { get; set; }
		[Display(Name = "LinkedIn", ResourceType = typeof(ApplicationStrings))]
		[DataType(DataType.Url)]
		public string LinkedIn { get; set; }
		[Display(Name = "GooglePlus", ResourceType = typeof(ApplicationStrings))]
		[DataType(DataType.Url)]
		public string GooglePlus { get; set; }
		public bool IsActive { get; set; }

		public bool IsFeatured { get; set; }

		public virtual User User { get; set; }
		public virtual ICollection<Job> Jobs { get; set; }
	}
}
