﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Core.Extensions
{
	public static class EnumExtensions
	{
		public static IEnumerable<T> ToEnumerable<T>(this T @this) where T : struct
		{
			return Enum.GetValues(@this.GetType()).Cast<T>().AsEnumerable();
		}

		public static string GetDisplayName<T>(this T @this)
		{
			var type = @this.GetType();
			var descriptionAttribute = type.GetMember(@this.ToString())[0].GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().SingleOrDefault();

			return descriptionAttribute != null
				? descriptionAttribute.Description
				: string.Empty;
		}

		public static T GetAttribute<T>(this object @this) where T : Attribute
		{
			var type = @this.GetType();

			return type.GetMember(@this.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();
		}
	}
}
