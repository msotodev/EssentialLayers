using EssentialLayers.Dapper.Helpers;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Services
{
	internal class MultipleProcedureService(
		IOptions<ConnectionOption> options
	) : IMultipleProcedure
	{
		private readonly ProcedureHelper _procedureHelper = new(options.Value.ConnectionString);

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
	}
}