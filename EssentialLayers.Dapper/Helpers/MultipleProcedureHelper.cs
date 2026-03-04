using Dapper;
using EssentialLayers.Dapper.Extension;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EssentialLayers.Dapper.Helpers
{
	public class MultipleProcedureHelper(string connectionString)
	{
		private readonly string ConnectionString = connectionString;

		public ResultHelper<IEnumerable<IEnumerable<dynamic>>> Execute<TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<IEnumerable<dynamic>>>.Fail(
				response.Message
			);

			DynamicParameters dynamicParameters = request.ParseDynamicParameters();

			using SqlConnection sqlConnection = new(ConnectionString);

			try
			{
				List<IEnumerable<dynamic>> resultSets = [];

				GridReader gridReader = sqlConnection.QueryMultiple(
					storedProcedure, dynamicParameters, commandTimeout: 0,
					commandType: CommandType.StoredProcedure
				);

				while (!gridReader.IsConsumed)
				{
					resultSets.Add(gridReader.Read());
				}

				return ResultHelper<IEnumerable<IEnumerable<dynamic>>>.Success(resultSets);
			}
			catch (Exception e)
			{
				return ResultHelper<IEnumerable<IEnumerable<dynamic>>>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		public async Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteAsync<TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<IEnumerable<dynamic>>>.Fail(
				response.Message
			);

			DynamicParameters dynamicParameters = request.ParseDynamicParameters();

			using SqlConnection sqlConnection = new(ConnectionString);

			try
			{
				List<IEnumerable<dynamic>> resultSets = [];

				GridReader gridReader = await sqlConnection.QueryMultipleAsync(
					storedProcedure, dynamicParameters, commandTimeout: 0, commandType: CommandType.StoredProcedure
				);

				while (!gridReader.IsConsumed)
				{
					resultSets.Add(gridReader.Read());
				}

				return ResultHelper<IEnumerable<IEnumerable<dynamic>>>.Success(resultSets);
			}
			catch (Exception e)
			{
				return ResultHelper<IEnumerable<IEnumerable<dynamic>>>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}
	}
}