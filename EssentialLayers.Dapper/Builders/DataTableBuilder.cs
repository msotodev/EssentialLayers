using EssentialLayers.Dapper.Cache;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EssentialLayers.Dapper.Builders
{
	public static class DataTableBuilder
	{
		public static DataTable Build<T>(this IEnumerable<T> source)
		{
			if (source == null) return new DataTable();

			using DataTable result = new();

			PropertyInfo[] properties = ReflectionCache.GetProperties<T>();

			foreach (PropertyInfo property in properties)
			{
				result.Columns.Add(property.Name, property.PropertyType);
			}

			foreach (T item in source)
			{
				DataRow dataRow = result.NewRow();

				foreach (PropertyInfo property in properties)
				{
					dataRow[property.Name] = property.GetValue(item)!;
				}

				result.Rows.Add(dataRow);
			}

			return result;
		}
	}
}