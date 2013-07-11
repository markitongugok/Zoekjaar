using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using Entities;

namespace Zoekjaar.Web.Models
{
	public class ViewCandidatesModel
	{
		public IEnumerable<CandidateView> Candidates { get; set; }
	}
}