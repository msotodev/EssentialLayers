using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Services.Connection;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services.Query
{
	internal class QueryService(
		IConnectionService connectionService,
		ILogger<QueryService> logger
	) : IQueryService
	{
		private QueryHelper _queryHelper = new(connectionService.Get());

		public ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, object? param = null
		)
		{
			ResultHelper<HashSet<ResultDto>> result = _queryHelper.QueryAll<ResultDto>(
				query, param
			);

			logger.LogInformation(query, param);

			return result;
		}

		public async Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, object? param = null
		)
		{
			ResultHelper<HashSet<ResultDto>> result = await _queryHelper.QueryAllAsync<ResultDto>(
				query, param
			);

			logger.LogInformation(query, param);

			return result;
		}

		public ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, object? param = null
		)
		{
			ResultHelper<ResultDto> result = _queryHelper.QueryFirst<ResultDto>(
				query, param
			);

			logger.LogInformation(query, param);

			return result;
		}

		public async Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, object? param = null
		)
		{
			ResultHelper<ResultDto> result = await _queryHelper.QueryFirstAsync<ResultDto>(
				query, param
			);

			logger.LogInformation(query, param);

			return result;
		}

		public void SetConnection(string connectionString)
		{
			_queryHelper = new QueryHelper(connectionString);
		}
	}
}