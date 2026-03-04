using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Interfaces
{
	public interface IComplexProcedure
	{
		ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		Task<ResultHelper<TResult>> ExecuteAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		);
	}
}