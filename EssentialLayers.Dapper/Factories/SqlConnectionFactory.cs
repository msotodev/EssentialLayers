using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EssentialLayers.Dapper.Factories
{
	internal class SqlConnectionFactory(
		IOptions<ConnectionOption> options
	) : IDbConnectionFactory
	{
		public string ConnectionString { get; } = options.Value.ConnectionString;

		public SqlConnection CreateConnection() => new(ConnectionString);
	}
}
