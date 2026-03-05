using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;

namespace EssentialLayers.Dapper.Validators
{
	internal static class ConnectionStringValidator
	{
		internal static Response Validate(string connectionString)
		{
			if (connectionString.IsEmpty()) return Response.Fail(
				"The connection string wasn't initialized yet"
			);

			return Response.Success();
		}
	}
}