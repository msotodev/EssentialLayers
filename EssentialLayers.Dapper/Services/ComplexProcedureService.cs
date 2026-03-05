using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services
{
	internal class ComplexProcedureService(
		IDbConnectionFactory connectionFactory
	) : IComplexProcedure
	{
		private readonly ComplexProcedureHelper _procedureHelper = new(connectionFactory);

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