namespace EssentialLayers.Request.Services.Factory
{
	public class HttpFactoryOptions
	{
		public string ClientName { get; set; } = string.Empty;

		public string BaseAddress { get; set; } = string.Empty;
		
		public string UserAgent { get; set; } = "MyApp/1.0";

		public string DefaultContentType { get; set; } = "application/json";
	}
}