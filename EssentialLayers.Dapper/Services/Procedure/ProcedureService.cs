using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Services.Connection;
using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services.Procedure
{
	internal class ProcedureService(IConnectionService connectionService) : IProcedureService
	{
		private ProcedureHelper _procedureHelper = new(connectionService.Get());

		public ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<TResult> result = _procedureHelper.Execute<TResult, TRequest>(
				request, storedProcedure
			);

			return result;
		}

		public async Task<ResultHelper<TResult>> ExecuteAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<TResult> result = await _procedureHelper.ExecuteAsync<TResult, TRequest>(
				request, storedProcedure
			);

			return result;
		}

		public ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<IEnumerable<TResult>> result = _procedureHelper.ExecuteAll<TResult, TRequest>(
				request, storedProcedure
			);

			return result;
		}

		public async Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<IEnumerable<TResult>> result = await _procedureHelper.ExecuteAllAsync<TResult, TRequest>(
				request, storedProcedure
			);

			return result;
		}

		public ResultHelper<TResult> ExecuteComplex<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<TResult> result = _procedureHelper.ExecuteComplex<TResult, TRequest>(request, storedProcedure);

			return result;
		}

		public async Task<ResultHelper<TResult>> ExecuteComplexAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<TResult> result = await _procedureHelper.ExecuteComplexAsync<TResult, TRequest>(
				request, storedProcedure
			);

			return result;
		}

		public ResultHelper<IEnumerable<TResult>> ExecuteComplexAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<IEnumerable<TResult>> result = _procedureHelper.ExecuteComplexAll<TResult, TRequest>(
				request, storedProcedure
			);

			return result;
		}

		public async Task<ResultHelper<IEnumerable<TResult>>> ExecuteComplexAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<IEnumerable<TResult>> result = await _procedureHelper.ExecuteComplexAllAsync<TResult, TRequest>(
				request, storedProcedure
			);

			return result;
		}

		public ResultHelper<IEnumerable<IEnumerable<dynamic>>> ExecuteMultiple<TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<IEnumerable<IEnumerable<dynamic>>> result = _procedureHelper.ExecuteMultiple(
				request, storedProcedure
			);

			return result;
		}

		public async Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteMultipleAsync<TRequest>(
			TRequest request, string storedProcedure
		)
		{
			ResultHelper<IEnumerable<IEnumerable<dynamic>>> result = await _procedureHelper.ExecuteMultipleAsync(
				request, storedProcedure
			);

			return result;
		}

		public void SetConnection(string connectionString)
		{
			_procedureHelper = new ProcedureHelper(connectionString);
		}
	}
}