using System;
using Zoekjaar.Resources;

namespace Zoekjaar.Web.Extensions
{
	public static class DateExtensions
	{
		public static string ToDescriptive(this DateTime date)
		{
			var difference = DateTime.Now.Subtract(date);

			if (difference.TotalSeconds < 60)
			{
				return ApplicationStrings.JustNow;
			}

			if (difference.TotalMinutes < 60)
			{
				return string.Format(ApplicationStrings.MinutesAgo, Math.Floor(difference.TotalMinutes), Math.Floor(difference.TotalMinutes) > 1 ? "s" : "");
			}

			if (difference.TotalHours < 24)
			{
				return string.Format(ApplicationStrings.HoursAgo, Math.Floor(difference.TotalHours), Math.Floor(difference.TotalHours) > 1 ? "s" : "");
			}

			if (difference.TotalDays < 30)
			{
				return string.Format(ApplicationStrings.DaysAgo, Math.Floor(difference.TotalDays), Math.Floor(difference.TotalDays) > 1 ? "s" : "");
			}

			return date.ToString("d");
		}
	}
}