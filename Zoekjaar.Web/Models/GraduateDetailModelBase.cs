using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoekjaar.Web.Models
{
	public abstract class GraduateDetailModelBase<TEntity>
	{
		public virtual TEntity Template { get; set; }

		public virtual IEnumerable<TEntity> Items { get; set; }
	}
}