using EssentialLayers.Helpers.Extension;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Services.Factory
{
	internal class AuthHeaderHandler(IFactoryTokenProvider tokenProvider) : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			string token = tokenProvider.GetToken();

			if (token.NotEmpty())
			{
				request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			}

			string apiKey = tokenProvider.GetApiKey();

			if (apiKey.NotEmpty())
			{
				request.Headers.Add("x-api-key", apiKey);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}
