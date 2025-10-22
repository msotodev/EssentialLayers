namespace EssentialLayers.Request.Services.Http
{
	internal class ClientFactoryTokenProvider : IClientFactoryTokenProvider
	{
		private string _token = string.Empty;

		public string GetToken() => _token;

		public void SetToken(string token) => _token = token;
	}
}