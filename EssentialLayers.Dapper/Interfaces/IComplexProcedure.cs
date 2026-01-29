using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Interfaces
{
	public interface IComplexProcedure
	{
		ResultHelper<TResult> ExecuteComplex<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		ResultHelper<IEnumerable<TResult>> ExecuteComplexAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		Task<ResultHelper<IEnumerable<TResult>>> ExecuteComplexAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		);
	}
}