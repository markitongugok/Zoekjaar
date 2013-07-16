using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zoekjaar.Resources;
namespace Business
{
	public sealed class GraduateView
	{
		public int GraduateId { get; set; }
		
		[Display(Name = "Name", ResourceType = typeof(ApplicationStrings))]
		public string Name { get; set; }

		[Display(Name = "CurrentStatus", ResourceType = typeof(ApplicationStrings))]
		public string CurrentStatus { get; set; }

		[Display(Name = "VisaStatus", ResourceType = typeof(ApplicationStrings))]
		public string VisaStatus { get; set; }

		[Display(Name = "ActiveApplications", ResourceType = typeof(ApplicationStrings))]
		public IEnumerable<Tuple<int, string>> Applications { get; set; }
	}
}
