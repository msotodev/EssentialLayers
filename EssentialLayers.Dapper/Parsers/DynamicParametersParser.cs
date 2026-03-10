using Dapper;
using EssentialLayers.Dapper.Builders;
using EssentialLayers.Dapper.Cache;
using EssentialLayers.Dapper.Mappers;
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

				if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
				{
					DbType dbType = property.PropertyType.ToDbType();

					dynamicParameters.Add(parameterName, value, dbType);
				}
				else
				{
					if (property.PropertyType.IsGenericType &&
						property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
					)
					{
						IEnumerable<T> enumerable = (value as IEnumerable<T>)!;
						dynamicParameters.Add(parameterName, enumerable.Build());
					}
					else
					{
						dynamicParameters.Add(parameterName, value.Build());
					}
				}
			}

			return dynamicParameters;
		}
	}
}