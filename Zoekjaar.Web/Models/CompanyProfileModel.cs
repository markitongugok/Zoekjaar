using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Zoekjaar.Web.Models
{
	public class CompanyProfileModel
	{
		public Company Company { get; set; }

		public bool? IsSuccessful { get; set; }
	}
}