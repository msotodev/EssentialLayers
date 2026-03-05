using Dapper;
using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Extension;
using EssentialLayers.Dapper.Parsers;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Helpers
{
	public class ComplexProcedureHelper(
		IDbConnectionFactory connectionFactory
	) : BaseProcedureHelper(connectionFactory)
	{
		public ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => Execute(
			connection =>
			{
				using SqlCommand command = BuildCommand(request, storedProcedure, connection);

				connection.Open();

				return command.GetResults<TResult>().FirstOrDefault()!;
			}
		);

		public Task<ResultHelper<TResult>> ExecuteAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => ExecuteAsync(
			async connection =>
			{
				using SqlCommand command = BuildCommand(request, storedProcedure, connection);

				connection.Open();

				return (await command.GetResultsAsync<TResult>()).FirstOrDefault()!;
			}
		);

		public ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => Execute(
			connection =>
			{
				using SqlCommand command = BuildCommand(request, storedProcedure, connection);

				connection.Open();

				return command.GetResults<TResult>();
			}
		);

		public Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => ExecuteAsync(
			async connection =>
			{
				using SqlCommand command = BuildCommand(request, storedProcedure, connection);

				connection.Open();

				return await command.GetResultsAsync<TResult>();
			}
		);

		private static SqlCommand BuildCommand<TRequest>(
			TRequest request, string storedProcedure, SqlConnection connection
		)
		{
			SqlCommand command = new(storedProcedure, connection)
			{
				CommandType = CommandType.StoredProcedure,
				CommandTimeout = 0
			};

			SqlParameter[] sqlParameters = request.Parse().ParseSqlParameters();

			command.Parameters.AddRange(sqlParameters);

			return command;
		}
	}
}