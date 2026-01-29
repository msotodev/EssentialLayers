using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Interfaces
{
	public interface IMultipleProcedure
	{
		ResultHelper<IEnumerable<IEnumerable<dynamic>>> ExecuteMultiple<TRequest>(
			TRequest request, string storedProcedure
		);

		Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteMultipleAsync<TRequest>(
			TRequest request, string storedProcedure
		);
	}
}