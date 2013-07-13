using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class GraduateDegreeModel
	{
		public GraduateDegree Template { get; set; }

		public IEnumerable<GraduateDegree> Degrees { get; set; }
	}
}