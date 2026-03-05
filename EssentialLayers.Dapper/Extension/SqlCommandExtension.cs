using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Extension
{
	public static class SqlCommandExtension
	{
		public static IEnumerable<T> GetResults<T>(
			this SqlCommand sqlCommand
		)
		{
			IList<T> result = [];

			using (SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow))
			{
				while (reader.Read())
				{
					T instance = Activator.CreateInstance<T>();
					ReadOnlyCollection<DbColumn> columns = reader.GetColumnSchema();

					foreach (DbColumn column in columns)
					{
						object value = reader.GetValue(column.ColumnName);

						instance!.GetType().GetProperty(column.ColumnName)!.SetValue(instance, value);
					}

					result.Add(instance);
				}
			}

			return result;
		}

		public static async Task<IEnumerable<T>> GetResultsAsync<T>(
			this SqlCommand sqlCommand
		)
		{
			IList<T> result = [];

			using (SqlDataReader executeReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow))
			{
				while (await executeReader.ReadAsync())
				{
					T instance = Activator.CreateInstance<T>();
					ReadOnlyCollection<DbColumn> columns = executeReader.GetColumnSchema();

					foreach (DbColumn column in columns)
					{
						object value = executeReader.GetValue(column.ColumnName);

						instance!.GetType().GetProperty(column.ColumnName)!.SetValue(instance, value);
					}

					result.Add(instance);
				}
			}

			return result;
		}

		public static SqlParameter[] ParseSqlParameters(
			this DynamicParameters dynamicParameters
		)
		{
			IReadOnlyList<string> names = [.. dynamicParameters.ParameterNames];
			int count = names.Count;

			if (count == 0) return [];

			SqlParameter[] rented = ArrayPool<SqlParameter>.Shared.Rent(count);

			try
			{
				for (int i = 0; i < count; i++)
				{
					rented[i] = new SqlParameter
					{
						ParameterName = names[i],
						Value = dynamicParameters.Get<object>(names[i])
					};
				}

				SqlParameter[] result = new SqlParameter[count];
				Array.Copy(rented, result, count);

				return result;
			}
			finally
			{
				ArrayPool<SqlParameter>.Shared.Return(rented, clearArray: true);
			}
		}
	}
}