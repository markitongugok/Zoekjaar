using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoekjaar.Resources;

namespace Business.Criteria
{
	public abstract class SearchCriteriaBase
	{
		[Display(Name = "Keyword", ResourceType = typeof(ApplicationStrings))]
		public virtual string Keyword { get; set; }

		public virtual int? EntityId { get; set; }

		public virtual int PageNumber { get; set; }

		public virtual int PageSize { get; set; }

		public virtual int? TotalRecords { get; set; }
	}
}
