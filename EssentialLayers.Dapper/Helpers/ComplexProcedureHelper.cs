using Dapper;
using EssentialLayers.Dapper.Extension;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Helpers
{
	public class ComplexProcedureHelper(
		string connectionString
	)
	{
		private readonly string ConnectionString = connectionString;

		public ResultHelper<TResult> ExecuteComplex<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<TResult>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);
			using SqlCommand command = new(storedProcedure, sqlConnection);

			DynamicParameters dynamicParameters = request.ParseDynamicParameters();
			SqlParameter[] sqlParameters = dynamicParameters.ParseSqlParameters();

			command.Parameters.AddRange(sqlParameters);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 0;

			try
			{
				sqlConnection.Open();

				TResult first = command.GetResults<TResult>().FirstOrDefault()!;

				return ResultHelper<TResult>.Success(first);
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
			finally
			{
				sqlConnection.Close();
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		public async Task<ResultHelper<TResult>> ExecuteComplexAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<TResult>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);
			using SqlCommand command = new(storedProcedure, sqlConnection);

			DynamicParameters dynamicParameters = request.ParseDynamicParameters();
			SqlParameter[] sqlParameters = dynamicParameters.ParseSqlParameters();

			command.Parameters.AddRange(sqlParameters);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 0;

			try
			{
				sqlConnection.Open();

				TResult first = (await command.GetResultsAsync<TResult>()).FirstOrDefault()!;

				return ResultHelper<TResult>.Success(first);
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
			finally
			{
				sqlConnection.Close();
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		public ResultHelper<IEnumerable<TResult>> ExecuteComplexAll<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<TResult>>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);
			using SqlCommand command = new(storedProcedure, sqlConnection);

			DynamicParameters dynamicParameters = request.ParseDynamicParameters();
			SqlParameter[] sqlParameters = dynamicParameters.ParseSqlParameters();

			command.Parameters.AddRange(sqlParameters);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 0;

			try
			{
				sqlConnection.Open();

				IEnumerable<TResult> results = command.GetResults<TResult>();

				return ResultHelper<IEnumerable<TResult>>.Success(results);
			}
			catch (Exception e)
			{
				return ResultHelper<IEnumerable<TResult>>.Fail(e);
			}
			finally
			{
				sqlConnection.Close();
				SqlConnection.ClearPool(sqlConnection);
			}
		}

		public async Task<ResultHelper<IEnumerable<TResult>>> ExecuteComplexAllAsync<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<TResult>>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);
			using SqlCommand command = new(storedProcedure, sqlConnection);

			DynamicParameters dynamicParameters = request.ParseDynamicParameters();
			SqlParameter[] sqlParameters = dynamicParameters.ParseSqlParameters();

			command.Parameters.AddRange(sqlParameters);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 0;

			try
			{
				sqlConnection.Open();

				IEnumerable<TResult> results = await command.GetResultsAsync<TResult>();

				return ResultHelper<IEnumerable<TResult>>.Success(results);
			}
			catch (Exception e)
			{
				return ResultHelper<IEnumerable<TResult>>.Fail(e);
			}
			finally
			{
				sqlConnection.Close();
				SqlConnection.ClearPool(sqlConnection);
			}
		}
	}
}