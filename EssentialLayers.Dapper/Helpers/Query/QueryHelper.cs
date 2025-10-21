using Dapper;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EssentialLayers.Dapper.Helpers.Query
{
	public class QueryHelper(string connectionString)
	{
		private readonly string ConnectionString = connectionString;

		/**/

		public ResultHelper<HashSet<ResultDto>> QueryAll<ResultDto>(
			string query, params object[] parameters
		)
		{
			ResultHelper<List<ResultDto>> result = ValidateConnectionString(
				new List<ResultDto>(), ConnectionString
			);

			if (result.Ok.False()) return ResultHelper<HashSet<ResultDto>>.Fail(result.Message);

			using SqlConnection sqlConnection = new(ConnectionString);

			IEnumerable<ResultDto> queryResults = sqlConnection.Query<ResultDto>(query, parameters);

			return ResultHelper<HashSet<ResultDto>>.Success(queryResults.ToHashSet());
		}

		public async Task<ResultHelper<HashSet<ResultDto>>> QueryAllAsync<ResultDto>(
			string query, params object[] parameters
		)
		{
			ResultHelper<List<ResultDto>> result = ValidateConnectionString(
				new List<ResultDto>(), ConnectionString
			);

			if (result.Ok.False()) return ResultHelper<HashSet<ResultDto>>.Fail(result.Message);

			using SqlConnection sqlConnection = new(ConnectionString);

			IEnumerable<ResultDto> queryResults = await sqlConnection.QueryAsync<ResultDto>(query, parameters);

			return ResultHelper<HashSet<ResultDto>>.Success(queryResults.ToHashSet());
		}

		public ResultHelper<ResultDto> QueryFirst<ResultDto>(
			string query, params object[] parameters
		)
		{
			ResultHelper<ResultDto> result = ValidateConnectionString(
				Activator.CreateInstance<ResultDto>(), ConnectionString
			);

			if (result.Ok.False()) return result;

			using SqlConnection sqlConnection = new(ConnectionString);

			ResultDto? first = sqlConnection.QueryFirst<ResultDto>(query, parameters);

			return ResultHelper<ResultDto>.Success(first);
		}

		public async Task<ResultHelper<ResultDto>> QueryFirstAsync<ResultDto>(
			string query, params object[] parameters
		)
		{
			ResultHelper<ResultDto> result = ValidateConnectionString(
				Activator.CreateInstance<ResultDto>(), ConnectionString
			);

			if (result.Ok.False()) return result;

			using SqlConnection sqlConnection = new(ConnectionString);

			ResultDto? first = await sqlConnection.QueryFirstAsync<ResultDto>(query, parameters);

			return ResultHelper<ResultDto>.Success(first);
		}

		private ResultHelper<TResult> ValidateConnectionString<TResult>(
			TResult result, string connectionString
		)
		{
			bool isEmpty = connectionString.IsEmpty();

			if (isEmpty) return ResultHelper<TResult>.Fail(
				"The connection string wasn't initilized yet"
			);

			using (SqlConnection sqlConnection = new(ConnectionString))
			{
				int affected = sqlConnection.QueryFirst<int>("SELECT 1");

				if (affected == 0) return ResultHelper<TResult>.Fail("Failed to connect to the database");
			}

			return ResultHelper<TResult>.Success(result);
		}
	}
}