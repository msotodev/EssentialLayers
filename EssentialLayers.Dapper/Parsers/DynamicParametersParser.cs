using Dapper;
using EssentialLayers.Dapper.Builders;
using EssentialLayers.Dapper.Mappers;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EssentialLayers.Dapper.Parsers
{
	public static class DynamicParametersParser
	{
		public static DynamicParameters Parse<T>(this T source)
		{
			if (source == null) return new DynamicParameters();

			List<PropertyInfo> properties = [.. source.GetType().GetProperties()];
			DynamicParameters dynamicParameters = new();

			properties.ForEach(property =>
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
					DataTable dataTable = enumerable.Build();

					dynamicParameters.Add(parameterName, dataTable);
				}
				else if (property.PropertyType.IsClass)
				{
					List<PropertyInfo> nestedProperties = [.. value.GetType().GetProperties()];

					nestedProperties.ForEach(nestedProperty =>
					{
						DbType dbType = nestedProperty.PropertyType.ToDbType();

						dynamicParameters.Add(parameterName, nestedProperty.GetValue(value), dbType);
					});
				}
			});

			return dynamicParameters;
		}
	}
}