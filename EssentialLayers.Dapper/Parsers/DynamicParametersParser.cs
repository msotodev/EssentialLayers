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

			PropertyInfo[] properties = ReflectionCache.GetProperties(source.GetType());
			DynamicParameters dynamicParameters = new();

			foreach (PropertyInfo property in properties)
			{
				object value = property.GetValue(source)!;
				string parameterName = $"@{property.Name}";

				if (property.PropertyType.IsValueType)
				{
					DbType dbType = property.PropertyType.ToDbType();

					dynamicParameters.Add(parameterName, value, dbType);
				}
				else if (
					property.PropertyType.IsGenericType &&
					property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
				)
				{
					IEnumerable<object> enumerable = (value as IEnumerable<object>)!;

					dynamicParameters.Add(parameterName, enumerable.Build());
				}
				else if (property.PropertyType.IsClass)
				{
					PropertyInfo[] nestedProperties = ReflectionCache.GetProperties(value.GetType());

					foreach (PropertyInfo nestedProperty in nestedProperties)
					{
						DbType dbType = nestedProperty.PropertyType.ToDbType();

						dynamicParameters.Add(parameterName, nestedProperty.GetValue(value), dbType);
					}
				}
			}

			return dynamicParameters;
		}
	}
}