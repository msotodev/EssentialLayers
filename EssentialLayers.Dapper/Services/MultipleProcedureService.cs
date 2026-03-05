using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services
{
	internal class MultipleProcedureService(
		IDbConnectionFactory connectionFactory
	) : IMultipleProcedure
	{
		private readonly MultipleProcedureHelper _procedureHelper = new(connectionFactory);

		public ResultHelper<IEnumerable<IEnumerable<dynamic>>> ExecuteMultiple<TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.Execute(
			request, storedProcedure
		);

		public Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteMultipleAsync<TRequest>(
			TRequest request, string storedProcedure
		) => _procedureHelper.ExecuteAsync(
			request, storedProcedure
		);
	}
}