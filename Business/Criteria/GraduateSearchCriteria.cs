using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoekjaar.Resources;

namespace Business.Criteria
{
	public sealed class GraduateSearchCriteria : SearchCriteriaBase
	{
		[Display(Name = "VisaStatus", ResourceType = typeof(ApplicationStrings))]
		public int? VisaStatusId { get; set; }

		[Display(Name = "CurrentStatus", ResourceType = typeof(ApplicationStrings))]
		public int? CurrentStatusId { get; set; }
	}
}
