using Microsoft.Data.SqlClient;

namespace EssentialLayers.Dapper.Abstractions
{
	public interface IDbConnectionFactory
	{
		SqlConnection CreateConnection();
		string ConnectionString { get; }
	}
}