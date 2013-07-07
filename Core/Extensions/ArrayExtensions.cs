using System;
using System.Collections.Generic;

namespace Core.Extensions
{
	public static class ArrayExtensions
	{
		public static bool IsEqualTo<T>(this T[] @this, T[] other)
		{
			return ArrayExtensions.IsEqualTo<T>(@this, other, EqualityComparer<T>.Default);
		}

		public static bool IsEqualTo<T>(this T[] @this, T[] other, IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}

			if (@this == null && other == null)
			{
				return true;
			}

			if (@this == null || other == null)
			{
				return false;
			}

			if (object.ReferenceEquals(@this, other))
			{
				return true;
			}

			if (@this.Length != other.Length)
			{
				return false;
			}

			for (var i = 0; i < @this.Length; i++)
			{
				if (!comparer.Equals(@this[i], other[i]))
				{
					return false;
				}
			}

			return true;
		}
	}
}
