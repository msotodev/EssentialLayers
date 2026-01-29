using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services
{
	internal class ProcedureService(
		IOptions<ConnectionOption> options
	) : IComplexProcedure, INormalProcedure, IMultipleProcedure
	{
		private readonly ProcedureHelper _procedureHelper = new(options.Value.ConnectionString);

		public ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.Execute<TResult, TRequest>(
			request, storedProcedure
		);

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

		public ResultHelper<TResult> ExecuteComplex<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteComplex<TResult, TRequest>(request, storedProcedure);

		public Task<ResultHelper<TResult>> ExecuteComplexAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteComplexAsync<TResult, TRequest>(
			request, storedProcedure
		);

		public ResultHelper<IEnumerable<TResult>> ExecuteComplexAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteComplexAll<TResult, TRequest>(
			request, storedProcedure
		);

		public Task<ResultHelper<IEnumerable<TResult>>> ExecuteComplexAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		) =>_procedureHelper.ExecuteComplexAllAsync<TResult, TRequest>(
			request, storedProcedure
		);

		public ResultHelper<IEnumerable<IEnumerable<dynamic>>> ExecuteMultiple<TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteMultiple(
			request, storedProcedure
		);

		public Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteMultipleAsync<TRequest>(
			TRequest request, string storedProcedure
		) =>_procedureHelper.ExecuteMultipleAsync(
			request, storedProcedure
		);

		public ResultHelper<TResult> Execute<TResult>(
			string storedProcedure
		) => _procedureHelper.Execute<TResult, object>(
			new(), storedProcedure
		);

		public Task<ResultHelper<TResult>> ExecuteAsync<TResult>(
			string storedProcedure
		) => _procedureHelper.ExecuteAsync<TResult, object>(
			new(), storedProcedure
		);

		public ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult>(
			string storedProcedure
		) => _procedureHelper.ExecuteAll<TResult, object>(
			new(), storedProcedure
		);

		public Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult>(
			string storedProcedure
		) => _procedureHelper.ExecuteAllAsync<TResult, object>(
			new(), storedProcedure
		);
	}
}