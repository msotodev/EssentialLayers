using Microsoft.Data.SqlClient;

namespace EssentialLayers.Dapper.Abstractions
{
	/// <summary>
	/// Defines a factory for creating database connections.
	/// </summary>
	public interface IDbConnectionFactory
	{
		/// <summary>
		/// Creates a new SQL connection.
		/// </summary>
		/// <returns>A new SqlConnection instance.</returns>
		SqlConnection CreateConnection();

		/// <summary>
		/// Gets the connection string used by this factory.
		/// </summary>
		string ConnectionString { get; }
	}
}