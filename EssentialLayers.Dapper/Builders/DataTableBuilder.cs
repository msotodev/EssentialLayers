using EssentialLayers.Dapper.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace EssentialLayers.Dapper.Builders
{
	internal static class DataTableBuilder
	{
		private static Type GetColumnType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		internal static DataTable Build<T>(this IEnumerable<T> source)
		{
			if (source == null) return new DataTable();

			using DataTable result = new();
			PropertyInfo[] properties = source.ToList().First().GetCachedProperties();

			foreach (PropertyInfo property in properties)
			{
				result.Columns.Add(property.Name, GetColumnType(property.PropertyType));
			}

			foreach (T item in source)
			{
				DataRow dataRow = result.NewRow();

				foreach (PropertyInfo property in properties)
				{
					dataRow[property.Name] = property.GetValue(item) ?? DBNull.Value;
				}

				result.Rows.Add(dataRow);
			}

			return result;
		}

		public static DataTable Build<T>(this T source)
		{
			if (source == null) return new DataTable();

			using DataTable result = new();
			PropertyInfo[] properties = source.GetCachedProperties();

			foreach (PropertyInfo property in properties)
			{
				result.Columns.Add(property.Name, GetColumnType(property.PropertyType));
			}

			DataRow dataRow = result.NewRow();

			foreach (PropertyInfo property in properties)
			{
				dataRow[property.Name] = property.GetValue(source) ?? DBNull.Value;
			}

			result.Rows.Add(dataRow);

			return result;
		}
	}
}