using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Interfaces
{
	/// <summary>
	/// Defines methods for executing raw SQL queries against a database.
	/// </summary>
	public interface IQueryService
	{
	/// <summary>
		/// Executes a query and returns all results as a HashSet.
		/// </summary>
		/// <typeparam name="ResultDto">The type to map the query results to.</typeparam>
		/// <param name="query">The SQL query to execute.</param>
		/// <param name="param">Optional parameters for the query.</param>
		/// <returns>A ResultHelper containing a HashSet of results or an error.</returns>
		ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, object? param = null
		);

		/// <summary>
		/// Asynchronously executes a query and returns all results as a HashSet.
		/// </summary>
		/// <typeparam name="ResultDto">The type to map the query results to.</typeparam>
		/// <param name="query">The SQL query to execute.</param>
		/// <param name="param">Optional parameters for the query.</param>
		/// <returns>A Task containing a ResultHelper with a HashSet of results or an error.</returns>
		Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, object? param = null
		);

		/// <summary>
		/// Executes a query and returns the first result.
		/// </summary>
		/// <typeparam name="ResultDto">The type to map the query result to.</typeparam>
		/// <param name="query">The SQL query to execute.</param>
		/// <param name="param">Optional parameters for the query.</param>
		/// <returns>A ResultHelper containing the first result or an error.</returns>
		ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, object? param = null
		);

		/// <summary>
		/// Asynchronously executes a query and returns the first result.
		/// </summary>
		/// <typeparam name="ResultDto">The type to map the query result to.</typeparam>
		/// <param name="query">The SQL query to execute.</param>
		/// <param name="param">Optional parameters for the query.</param>
		/// <returns>A Task containing a ResultHelper with the first result or an error.</returns>
		Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, object? param = null
		);
	}
}