using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Extensions;
using Zoekjaar.Resources;

namespace Zoekjaar.Web.Extensions
{
	public static class EnumExtensions
	{
		public static SelectList ToSelectList(this object @this)
		{
			var items = Enum.GetValues(@this.GetType())
				.Cast<object>()
				.Select(_ => new Tuple<int?, string>((int)_, _.GetDisplayName())).ToList();
			items.Insert(0, new Tuple<int?, string>(null, ApplicationStrings.Select));
			return new SelectList(items, "Item1", "Item2");
		}
	}
}