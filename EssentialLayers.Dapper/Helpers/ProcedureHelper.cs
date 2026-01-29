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
using static Dapper.SqlMapper;

namespace EssentialLayers.Dapper.Helpers
{
	public class ProcedureHelper(string connectionString)
	{
		private readonly string ConnectionString = connectionString;

		public ResultHelper<TResult> Execute<TResult, TRequest>(
			TRequest request, string storedProcedure
		)
		{
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<TResult>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);

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
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<TResult>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);

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
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<TResult>>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);

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
			Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

			if (response.Ok.False()) return ResultHelper<IEnumerable<TResult>>.Fail(response.Message);

			using SqlConnection sqlConnection = new(ConnectionString);

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

		public ResultHelper<IEnumerable<IEnumerable<dynamic>>> ExecuteMultiple<TRequest>(
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

		public async Task<ResultHelper<IEnumerable<IEnumerable<dynamic>>>> ExecuteMultipleAsync<TRequest>(
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