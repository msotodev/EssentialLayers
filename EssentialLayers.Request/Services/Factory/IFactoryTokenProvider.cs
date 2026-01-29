namespace EssentialLayers.Request.Services.Factory
{
	public interface IFactoryTokenProvider
	{
		string GetToken();

		string GetApiKey();

		string GetHeaderApiKey();

		void SetToken(string token);

		void SetApiKey(string apiKey);

		void SetHeaderApiKey(string apyKey);
	}
}