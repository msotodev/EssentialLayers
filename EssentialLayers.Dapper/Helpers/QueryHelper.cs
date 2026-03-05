using Dapper;
using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Helpers
{
	public class QueryHelper(
		ILogger<QueryHelper> logger,
		IDbConnectionFactory connectionFactory
	) : BaseProcedureHelper(connectionFactory)
	{
		public ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, object? param = null
		) => Execute<HashSet<ResultDto>>(
			connection =>
			{
				logger.LogInformation(query, param);

				IEnumerable<ResultDto> results = connection.Query<ResultDto>(query, param);

				logger.LogInformation(results.Serialize(true));

				return [.. results];
			}
		);

		public Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, object? param = null
		) => ExecuteAsync<HashSet<ResultDto>>(
			async connection =>
			{
				logger.LogInformation(query, param);

				IEnumerable<ResultDto> results = await connection.QueryAsync<ResultDto>(query, param);

				logger.LogInformation(results.Serialize(true));

				return [.. results];
			}
		);

		public ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, object? param = null
		) => Execute(
			connection =>
			{
				logger.LogInformation(query, param);

				ResultDto first = connection.QueryFirst<ResultDto>(query, param);

				logger.LogInformation(first.Serialize());

				return first;
			}
		);

		public Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, object? param = null
		) => ExecuteAsync(
			async connection =>
			{
				logger.LogInformation(query, param);

				ResultDto first = await connection.QueryFirstAsync<ResultDto>(query, param);

				logger.LogInformation(first.Serialize());

				return first;
			}
		);
	}
}