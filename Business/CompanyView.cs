using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoekjaar.Resources;

namespace Business
{
	public sealed class CompanyView
	{
		public int Id { get; set; }
		
		[Display(Name = "Name", ResourceType = typeof(ApplicationStrings))]
		public string Name { get; set; }
		
		[Display(Name = "Website", ResourceType = typeof(ApplicationStrings))]
		public string Website { get; set; }

		[Display(Name = "Logo", ResourceType = typeof(ApplicationStrings))]
		public string LogoUrl { get; set; }
	}
}
