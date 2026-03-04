using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services
{
	internal class NormalProcedureService(
		IOptions<ConnectionOption> options
	) : INormalProcedure
	{
		private readonly NormalProcedureHelper _procedureHelper = new(options.Value.ConnectionString);

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