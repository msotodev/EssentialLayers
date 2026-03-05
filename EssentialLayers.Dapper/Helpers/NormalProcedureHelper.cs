using Dapper;
using EssentialLayers.Dapper.Abstractions;
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
	public class NormalProcedureHelper(
		IDbConnectionFactory connectionFactory
	)
	{
		public ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(connectionFactory.ConnectionString);

			if (response.Ok.False()) return ResultHelper<TResult>.Fail(response.Message);

			using SqlConnection sqlConnection = new(connectionFactory.ConnectionString);

			try
			{
				DynamicParameters dynamicParameters = request.ParseDynamicParameters();

				TResult query = sqlConnection.QueryFirst<TResult>(
					storedProcedure, dynamicParameters, commandTimeout: 0,
					commandType: CommandType.StoredProcedure
				);

				return ResultHelper<TResult>.Success(query);
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		public async Task<ResultHelper<TResult>> ExecuteAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(connectionFactory.ConnectionString);

			if (response.Ok.False()) return ResultHelper<TResult>.Fail(response.Message);

			using SqlConnection sqlConnection = new(connectionFactory.ConnectionString);

			try
			{
				DynamicParameters dynamicParameters = request.ParseDynamicParameters();

				TResult query = await sqlConnection.QueryFirstAsync<TResult>(
					storedProcedure, dynamicParameters, commandTimeout: 0,
					commandType: CommandType.StoredProcedure
				);

				return ResultHelper<TResult>.Success(query);
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		public ResultHelper<IEnumerable<TResult>> ExecuteAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(connectionFactory.ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<TResult>>.Fail(response.Message);

			using SqlConnection sqlConnection = new(connectionFactory.ConnectionString);

			try
			{
				DynamicParameters dynamicParameters = request.ParseDynamicParameters();

				IEnumerable<TResult> query = sqlConnection.Query<TResult>(
					storedProcedure, dynamicParameters, commandTimeout: 0,
					commandType: CommandType.StoredProcedure
				);

				return ResultHelper<IEnumerable<TResult>>.Success(query);
			}
			catch (Exception e)
			{
				return ResultHelper<IEnumerable<TResult>>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		public async Task<ResultHelper<IEnumerable<TResult>>> ExecuteAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(connectionFactory.ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<TResult>>.Fail(response.Message);

			using SqlConnection sqlConnection = new(connectionFactory.ConnectionString);

			try
			{
				DynamicParameters dynamicParameters = request.ParseDynamicParameters();

				IEnumerable<TResult> query = await sqlConnection.QueryAsync<TResult>(
					storedProcedure, dynamicParameters, commandTimeout: 0,
					commandType: CommandType.StoredProcedure
				);

				return ResultHelper<IEnumerable<TResult>>.Success(query);
			}
			catch (Exception e)
			{
				return ResultHelper<IEnumerable<TResult>>.Fail(e);
			}
			finally
			{
				SqlConnection.ClearPool(sqlConnection);
			}
		}
	}
}