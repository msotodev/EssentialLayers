using Dapper;
using EssentialLayers.Dapper.Builders;
using EssentialLayers.Dapper.Cache;
using EssentialLayers.Dapper.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EssentialLayers.Dapper.Parsers
{
	internal static class DynamicParametersParser
	{
		internal static DynamicParameters Parse<T>(this T source)
		{
			if (source == null) return new DynamicParameters();

			PropertyInfo[] properties = source.GetCachedProperties();
			DynamicParameters dynamicParameters = new();

			foreach (PropertyInfo property in properties)
			{
				object value = property.GetValue(source)!;
				string parameterName = $"@{property.Name}";
				Type type = property.PropertyType;

				// Value types: int, bool, DateTime, Guid, enums, etc.
				if (type.IsValueType)
				{
					DbType dbType = type.ToDbType();
					dynamicParameters.Add(parameterName, value, dbType);
				}
				// String: In C# is a class but is mapped directly as a DbType.String
				else if (type == typeof(string))
				{
					dynamicParameters.Add(parameterName, value, DbType.String);
				}
				// List<T>: converted as DataTable to TVP (Table-Valued Parameter)
				else if (
					type.IsGenericType &&
					type.GetGenericTypeDefinition() == typeof(List<>)
				)
				{
					IEnumerable<object> enumerable = (value as IEnumerable<object>)!;
					dynamicParameters.Add(parameterName, enumerable.Build());
				}
				// Complex classes: converted as DataTable with single row to TVP
				else if (type.IsClass && value != null)
				{
					IEnumerable<object> singleRow = [value];
					dynamicParameters.Add(parameterName, singleRow.Build());
				}
			}

			return dynamicParameters;
		}
	}
}