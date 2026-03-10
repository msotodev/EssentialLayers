namespace EssentialLayers.Dapper.Options
{
	/// <summary>
	/// Configuration options for database connection.
	/// </summary>
	public sealed class ConnectionOption
	{
		/// <summary>
		/// Gets or sets the database connection string.
		/// </summary>
		public string ConnectionString { get; set; } = string.Empty;
	}
}