using Dapper;
using EssentialLayers.Dapper.Validators;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Data.SqlClient;

namespace EssentialLayers.Dapper.Helpers
{
	internal static class ConnectionHelper
	{
		internal static Response ValidateConnectionString(
			string connectionString
		)
		{
			Response validationResponse = ConnectionStringValidator.Validate(connectionString);

			if (validationResponse.Ok.False()) return validationResponse;

			using SqlConnection sqlConnection = new(connectionString);

			int affected = sqlConnection.QueryFirst<int>("SELECT 1");

			if (affected == 0) return Response.Fail("Failed to connect to the database");

			return Response.Success();
		}
	}
}