using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public sealed class CandidateView
	{
		public int GraduateId { get; set; }
		public string Name { get; set; }
		public int? RecruitmentStatusId { get; set; }
		public string RecruitmentStatus { get; set; }
	}
}
