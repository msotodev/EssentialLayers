namespace EssentialLayers.Request.Services.Factory
{
	internal class FactoryTokenProvider : IFactoryTokenProvider
	{
		private string _token = string.Empty;

		public string GetToken() => _token;

		public void SetToken(string token) => _token = token;
	}
}