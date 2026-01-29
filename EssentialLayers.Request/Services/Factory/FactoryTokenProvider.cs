namespace EssentialLayers.Request.Services.Factory
{
	internal class FactoryTokenProvider : IFactoryTokenProvider
	{
		private string _token = string.Empty;

		private string _apiKey = string.Empty;

		private string _headerApiKey = string.Empty;

		public string GetToken() => _token;

		public string GetApiKey() => _apiKey;

		public string GetHeaderApiKey() => _headerApiKey;

		public void SetToken(string token) => _token = token;

		public void SetApiKey(string apyKey) => _apiKey = apyKey;

		public void SetHeaderApiKey(string apyKey) => _headerApiKey = apyKey;
	}
}