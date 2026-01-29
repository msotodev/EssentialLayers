using Dapper;
using EssentialLayers.Dapper.Extension;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Helpers
{
	public class QueryHelper(string connectionString)
	{
		private readonly string ConnectionString = connectionString;

		public ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, object? param = null
		)
		{
			try
			{
				Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

				if (response.Ok.False()) return ResultHelper<HashSet<ResultDto>>.Fail(response.Message);

				using SqlConnection sqlConnection = new(ConnectionString);

				IEnumerable<ResultDto> queryResults = sqlConnection.Query<ResultDto>(query, param);

				return ResultHelper<HashSet<ResultDto>>.Success([.. queryResults]);
			}
			catch (Exception e)
			{
				return e.HandleException<HashSet<ResultDto>>();
			}
		}

		public async Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, object? param = null
		)
		{
			try
			{
				Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

				if (response.Ok.False()) return ResultHelper<HashSet<ResultDto>>.Fail(response.Message);

				using SqlConnection sqlConnection = new(ConnectionString);

				IEnumerable<ResultDto> queryResults = await sqlConnection.QueryAsync<ResultDto>(
					query, param
				);

				return ResultHelper<HashSet<ResultDto>>.Success([.. queryResults]);
			}
			catch (Exception e)
			{
				return e.HandleException<HashSet<ResultDto>>();
			}
		}

		public ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, object? param = null
		)
		{
			try
			{
				Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

				if (response.Ok.False()) return ResultHelper<ResultDto>.Fail(response.Message);

				using SqlConnection sqlConnection = new(ConnectionString);

				ResultDto? first = sqlConnection.QueryFirst<ResultDto>(query, param);

				return ResultHelper<ResultDto>.Success(first);
			}
			catch (Exception e)
			{
				return e.HandleException<ResultDto>();
			}
		}

		public async Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, object? param = null
		)
		{
			try
			{
				Response response = ConnectionHelper.ValidateConnectionString(ConnectionString);

				if (response.Ok.False()) return ResultHelper<ResultDto>.Fail(response.Message);

				using SqlConnection sqlConnection = new(ConnectionString);

				ResultDto? first = await sqlConnection.QueryFirstAsync<ResultDto>(query, param);

				return ResultHelper<ResultDto>.Success(first);
			}
			catch (Exception e)
			{
				return e.HandleException<ResultDto>();
			}
		}
	}
}