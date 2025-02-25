using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services.Query
{
	public interface IQueryService
	{
		ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, params object[] parameters
		);

		Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, params object[] parameters
		);

		ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, params object[] parameters
		);

		Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, params object[] parameters
		);
	}
}