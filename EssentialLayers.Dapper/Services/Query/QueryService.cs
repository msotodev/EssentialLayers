using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Helpers.Query;
using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services.Query
{
	internal class QueryService : IQueryService
	{
		private QueryHelper _queryHelper;

		/**/

		public QueryService()
		{
			string connectionString = Tools.Get.ConnectionService!.Get();

			_queryHelper = new QueryHelper(connectionString);
		}

		public ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(string query, params object[] parameters)
		{
			ResultHelper<HashSet<ResultDto>> result = _queryHelper.QueryAll<ResultDto>(
				query, parameters
			);

			return result;
		}

		public async Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, params object[] parameters
		)
		{
			ResultHelper<HashSet<ResultDto>> result = await _queryHelper.QueryAllAsync<ResultDto>(
				query, parameters
			);

			return result;
		}

		public ResultHelper<ResultDto> QueryFirst<ResultDto>(string query, params object[] parameters)
		{
			ResultHelper<ResultDto> result = _queryHelper.QueryFirst<ResultDto>(
				query, parameters
			);

			return result;
		}

		public async Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, params object[] parameters
		)
		{
			ResultHelper<ResultDto> result = await _queryHelper.QueryFirstAsync<ResultDto>(
				query, parameters
			);

			return result;
		}

		public void SetConnection(string connectionString)
		{
			_queryHelper = new QueryHelper(connectionString);
		}
	}
}