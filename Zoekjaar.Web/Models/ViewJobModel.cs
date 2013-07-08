using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using Entities;

namespace Zoekjaar.Web.Models
{
	public sealed class ViewJobModel
	{
		public Company Company { get; set; }
		public JobView Job { get; set; }
	}
}