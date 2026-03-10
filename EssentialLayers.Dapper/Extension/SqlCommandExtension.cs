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
	/// <summary>
	/// Provides extension methods for SqlCommand to read results.
	/// </summary>
	public static class SqlCommandExtension
	{
		/// <summary>
		/// Reads results from a SqlCommand execution.
		/// </summary>
		/// <typeparam name="T">The type to map the results to.</typeparam>
		/// <param name="sqlCommand">The SqlCommand to read from.</param>
		/// <returns>An enumerable of results.</returns>
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

		/// <summary>
		/// Asynchronously reads results from a SqlCommand execution.
		/// </summary>
		/// <typeparam name="T">The type to map the results to.</typeparam>
		/// <param name="sqlCommand">The SqlCommand to read from.</param>
		/// <returns>A Task containing an enumerable of results.</returns>
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

		/// <summary>
		/// Converts DynamicParameters to an array of SqlParameter.
		/// </summary>
		/// <param name="dynamicParameters">The DynamicParameters to convert.</param>
		/// <returns>An array of SqlParameter.</returns>
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