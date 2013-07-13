using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;

namespace Zoekjaar.Web.Models
{
	public sealed class JobApplicationModel
	{
		public IEnumerable<JobApplicationView> Jobs { get; set; }
	}
}