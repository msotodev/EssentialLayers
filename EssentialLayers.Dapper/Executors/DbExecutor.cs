using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Executors
{
	internal static class DbExecutor
	{
		internal static ResultHelper<T> Execute<T>(
			IDbConnectionFactory connectionFactory,
			Func<SqlConnection, T> operation
		)
		{
			Response response = Helpers.ConnectionHelper.ValidateConnectionString(
				connectionFactory.ConnectionString
			);

			if (response.Ok.False()) return ResultHelper<T>.Fail(response.Message);

			using SqlConnection sqlConnection = connectionFactory.CreateConnection();

			try
			{
				return ResultHelper<T>.Success(operation(sqlConnection));
			}
			catch (Exception e)
			{
				return ResultHelper<T>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		internal static async Task<ResultHelper<T>> ExecuteAsync<T>(
			IDbConnectionFactory connectionFactory,
			Func<SqlConnection, Task<T>> operation
		)
		{
			Response response = Helpers.ConnectionHelper.ValidateConnectionString(
				connectionFactory.ConnectionString
			);

			if (response.Ok.False()) return ResultHelper<T>.Fail(response.Message);

			using SqlConnection sqlConnection = connectionFactory.CreateConnection();

			try
			{
				return ResultHelper<T>.Success(await operation(sqlConnection));
			}
			catch (Exception e)
			{
				return ResultHelper<T>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}
	}
}