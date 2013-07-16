using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoekjaar.Web.Models
{
	public sealed class GraduateLinkModel
	{
		public string LinkedIn { get; set; }

		public string GooglePlus { get; set; }

		public bool? IsSuccessful { get; set; }
	}
}