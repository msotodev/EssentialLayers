using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Interfaces
{
	/// <summary>
	/// Defines methods for executing stored procedures that return multiple result sets.
	/// </summary>
	public interface IMultipleProcedure
	{
		/// <summary>
		/// Executes a stored procedure that returns multiple result sets.
		/// </summary>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A ResultHelper containing an enumerable of result sets or an error.</returns>
		ResultHelper<IEnumerable<IEnumerable<dynamic>>> ExecuteMultiple<TRequest>(
			TRequest request, string storedProcedure
		);

		/// <summary>
		/// Asynchronously executes a stored procedure that returns multiple result sets.
		/// </summary>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A Task containing a ResultHelper with an enumerable of result sets or an error.</returns>
		Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteMultipleAsync<TRequest>(
			TRequest request, string storedProcedure
		);
	}
}