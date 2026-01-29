using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services.Query
{
	public interface IQueryService
	{
		ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, object? param = null
		);

		Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, object? param = null
		);

		ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, object? param = null
		);

		Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, object? param = null
		);
	}
}