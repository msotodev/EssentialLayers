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
	internal class MultipleProcedureHelper(
		IDbConnectionFactory connectionFactory
	) : BaseProcedureHelper(connectionFactory)
	{
		public ResultHelper<IEnumerable<IEnumerable<dynamic>>> Execute<TRequest>(
			TRequest request, string storedProcedure
		) => Execute<IEnumerable<IEnumerable<dynamic>>>(
			connection =>
			{
				DynamicParameters parameters = request.Parse();
				List<IEnumerable<dynamic>> resultSets = [];

				GridReader gridReader = connection.QueryMultiple(
					storedProcedure, parameters,
					commandTimeout: 0, commandType: CommandType.StoredProcedure
				);

				while (!gridReader.IsConsumed) resultSets.Add(gridReader.Read());

				return resultSets;
			}
		);

		public Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteAsync<TRequest>(
			TRequest request, string storedProcedure
		) => ExecuteAsync<IEnumerable<IEnumerable<dynamic>>>(
			async connection =>
			{
				DynamicParameters parameters = request.Parse();
				List<IEnumerable<dynamic>> resultSets = [];

				GridReader gridReader = await connection.QueryMultipleAsync(
					storedProcedure, parameters,
					commandTimeout: 0, commandType: CommandType.StoredProcedure
				);

				while (!gridReader.IsConsumed) resultSets.Add(gridReader.Read());

				return resultSets;
			}
		);
	}
}