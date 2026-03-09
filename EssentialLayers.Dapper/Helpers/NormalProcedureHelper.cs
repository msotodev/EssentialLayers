using Dapper;
using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Parsers;
using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EssentialLayers.Dapper.Helpers
{
	internal class NormalProcedureHelper(
		IDbConnectionFactory connectionFactory
	) : BaseProcedureHelper(connectionFactory)
	{
		private const CommandType _commandType = CommandType.StoredProcedure;

		private const int commandTimeout = 0;

		public ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => Execute(
			connection => connection.QueryFirst<TResult>(
				storedProcedure, request.Parse(),
				commandTimeout: commandTimeout, commandType: _commandType
			)
		);

		public Task<ResultHelper<TResult>> ExecuteAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => ExecuteAsync(
			connection => connection.QueryFirstAsync<TResult>(
				storedProcedure, request.Parse(),
				commandTimeout: commandTimeout, commandType: _commandType
			)
		);

		public ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => Execute(
			connection => connection.Query<TResult>(
				storedProcedure, request.Parse(),
				commandTimeout: commandTimeout, commandType: _commandType
			)
		);

		public Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => ExecuteAsync(
			connection => connection.QueryAsync<TResult>(
				storedProcedure, request.Parse(),
				commandTimeout: commandTimeout, commandType: _commandType
			)
		);
	}
}