using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime SpecifyDateAsUtc(this DateTime dateTime)
		{
			return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
		}


		public static DateTime GetMaxBetween(DateTime first, DateTime second)
		{
			return first > second
				? first
				: second;
		}
	}
}
