namespace EssentialLayers.Request.Services.Factory
{
	public interface IFactoryTokenProvider
	{
		string GetToken();

		void SetToken(string token);
	}
}