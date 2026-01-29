using Dapper;
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
			bool isEmpty = connectionString.IsEmpty();

			if (isEmpty) return Response.Fail(
				"The connection string wasn't initilized yet"
			);

			using (SqlConnection sqlConnection = new(connectionString))
			{
				int affected = sqlConnection.QueryFirst<int>("SELECT 1");

				if (affected == 0) return Response.Fail("Failed to connect to the database");
			}

			return Response.Success();
		}
	}
}