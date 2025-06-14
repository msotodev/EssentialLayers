﻿using System;
using System.Globalization;

namespace EssentialLayers.Helpers.Extension
{
	public static class DateTimeExtension
	{
		public static readonly DateTime DEFAULT = new(1900, 1, 1);

		public static string ToShortFormatMX(this DateTime value, bool bWithSlash = true)
		{
			if (bWithSlash) return Convert.ToDateTime(
				value.ToString()
			).ToString("dd/MM/yyyy");

			return Convert.ToDateTime(value.ToString()).ToString("dd-MM-yyyy");
		}

		public static string ToFullFormatMX(this DateTime value, bool bWithSlash = true)
		{
			if (bWithSlash) return Convert.ToDateTime(
				value.ToString()
			).ToString("dd/MM/yyyy hh:mm:ss tt");

			return Convert.ToDateTime(value.ToString()).ToString("dd-MM-yyyy hh:mm:ss tt");
		}

		public static bool IsEqualsToDefault(this DateTime datetime, bool full = true)
		{
			DateTime date = datetime;

			if (!full) date = DateTime.Parse(datetime.ToShortDateString());

			int value = DateTime.Compare(DEFAULT, date);

			return value == 0;
		}

		public static string EqualsEmpty(this DateTime datetime, string date)
		{
			int value = DateTime.Compare(DEFAULT, datetime);

			return value == 0 ? string.Empty : date;
		}

		public static string StringDay(this DateTime dateTime, string culture = "es-MX")
		{
			CultureInfo cultureInfo = new(culture);

			return dateTime.ToString("dddd", cultureInfo);
		}

		public static string StringMonth(this DateTime dateTime, string culture = "es-MX")
		{
			CultureInfo cultureInfo = new(culture);

			return dateTime.ToString("MMMM", cultureInfo);
		}

		public static string StringYear(this DateTime dateTime, string culture = "es-MX")
		{
			CultureInfo cultureInfo = new(culture);

			return dateTime.ToString("yyyy", cultureInfo);
		}

		public static string StringTime(
			this DateTime dateTime, TimeFormat timeFormat = TimeFormat._TWELVE_HOURS,
			bool includeAmPm = true, string culture = "es-MX"
		)
		{
			CultureInfo cultureInfo = new(culture);
			string format = GetTimeFormat(timeFormat);
			string ampm = includeAmPm ? "tt" : string.Empty;

			return dateTime.ToString($"{format}:mm:ss {ampm}", cultureInfo);
		}

		private static string GetTimeFormat(TimeFormat timeFormat) => timeFormat switch
		{
			TimeFormat._TWELVE_HOURS => "hh",
			TimeFormat._TWENTY_FOUR_HOURS => "HH",
			_ => "hh",
		};

		public enum TimeFormat
		{
			_TWELVE_HOURS,
			_TWENTY_FOUR_HOURS
		}
	}
}