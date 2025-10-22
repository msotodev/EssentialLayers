namespace EssentialLayers.Request.Services.Http
{
	public interface IClientFactoryTokenProvider
	{
		string GetToken();

		void SetToken(string token);
	}
}