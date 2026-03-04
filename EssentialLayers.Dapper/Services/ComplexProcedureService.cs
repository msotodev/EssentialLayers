using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services
{
	internal class ComplexProcedureService(
		IOptions<ConnectionOption> options
	) : IComplexProcedure
	{
		private readonly ComplexProcedureHelper _procedureHelper = new(options.Value.ConnectionString);

		public ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.Execute<TResult, TRequest>(request, storedProcedure);

		public Task<ResultHelper<TResult>> ExecuteAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteAsync<TResult, TRequest>(
			request, storedProcedure
		);

		public ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteAll<TResult, TRequest>(
			request, storedProcedure
		);

		public Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteAllAsync<TResult, TRequest>(
			request, storedProcedure
		);
	}
}