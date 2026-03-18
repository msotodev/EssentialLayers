using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Interfaces
{
	/// <summary>
	/// Defines methods for executing complex stored procedures that use SqlCommand directly.
	/// Supports multiple result sets and table-valued parameters.
	/// </summary>
	public interface IComplexProcedure
	{
		/// <summary>
		/// Executes a complex stored procedure and returns the first result.
		/// </summary>
		/// <typeparam name=" TResult">The type to map the result to.</typeparam>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A ResultHelper containing the first result or an error.</returns>
		ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		/// <summary>
		/// Executes a complex stored procedure and returns Response.
		/// </summary>
		/// <typeparam name="Response">The type to map the result to.</typeparam>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A ResultHelper containing the first result or an error.</returns>
		Response Execute<TRequest>(
			TRequest request, string storedProcedure
		);

		/// <summary>
		/// Asynchronously executes a complex stored procedure and returns the first result.
		/// </summary>
		/// <typeparam name="TResult">The type to map the result to.</typeparam>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A Task containing a ResultHelper with the first result or an error.</returns>
		Task<ResultHelper<TResult>> ExecuteAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		/// <summary>
		/// Asynchronously executes a complex stored procedure and returns Response.
		/// </summary>
		/// <typeparam name="Response">The type to map the result to.</typeparam>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A Task containing a ResultHelper with the first result or an error.</returns>
		Task<Response> ExecuteAsync<TRequest>(
			TRequest request, string storedProcedure
		);

		/// <summary>
		/// Executes a complex stored procedure and returns all results.
		/// </summary>
		/// <typeparam name="TResult">The type to map the results to.</typeparam>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A ResultHelper containing all results or an error.</returns>
		ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		/// <summary>
		/// Asynchronously executes a complex stored procedure and returns all results.
		/// </summary>
		/// <typeparam name="TResult">The type to map the results to.</typeparam>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A Task containing a ResultHelper with all results or an error.</returns>
		Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		);

		/// <summary>
		/// Asynchronously executes a complex stored procedure and returns Response.
		/// </summary>
		/// <typeparam name="Response">The type to map the results to.</typeparam>
		/// <typeparam name="TRequest">The type of the request object containing parameters.</typeparam>
		/// <param name="request">The request object with parameters for the stored procedure.</param>
		/// <param name="storedProcedure">The name of the stored procedure to execute.</param>
		/// <returns>A Task containing a ResultHelper with all results or an error.</returns>
		Task<Response> ExecuteAllAsync<TRequest>(
			TRequest request, string storedProcedure
		);
	}
}