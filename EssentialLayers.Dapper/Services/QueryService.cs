using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services
{
	internal class QueryService(
		IOptions<ConnectionOption> options,
		ILogger<QueryHelper> logger
	) : IQueryService
	{
		private readonly QueryHelper _queryHelper = new(
			logger, options.Value.ConnectionString
		);

		public ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, object? param = null
		) => _queryHelper.QueryAll<ResultDto>(query, param);

		public Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, object? param = null
		) => _queryHelper.QueryAllAsync<ResultDto>(query, param);

		public ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, object? param = null
		) => _queryHelper.QueryFirst<ResultDto>(query, param);

		public Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, object? param = null
		) =>  _queryHelper.QueryFirstAsync<ResultDto>(query, param);
	}
}