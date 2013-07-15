using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Criteria
{
	public sealed class GraduateSearchCriteria : SearchCriteriaBase
	{
		public int? VisaStatusId { get; set; }
		public int? CurrentStatusId { get; set; }
	}
}
