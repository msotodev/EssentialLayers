using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Executors;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Helpers
{
	public abstract class BaseProcedureHelper(
		IDbConnectionFactory connectionFactory
	)
	{
		protected ResultHelper<T> Execute<T>(
			Func<SqlConnection, T> operation
		) => DbExecutor.Execute(connectionFactory, operation);

		protected Task<ResultHelper<T>> ExecuteAsync<T>(
			Func<SqlConnection, Task<T>> operation
		) => DbExecutor.ExecuteAsync(connectionFactory, operation);
	}
}