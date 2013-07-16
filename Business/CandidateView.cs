using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoekjaar.Resources;

namespace Business
{
	public sealed class CandidateView
	{
		public int GraduateId { get; set; }
		[Display(Name = "Name", ResourceType = typeof(ApplicationStrings))]
		public string Name { get; set; }

		[Display(Name = "RecruitmentStatus", ResourceType = typeof(ApplicationStrings))]
		public int? RecruitmentStatusId { get; set; }

		[Display(Name = "RecruitmentStatus", ResourceType = typeof(ApplicationStrings))]
		public string RecruitmentStatus { get; set; }
	}
}
