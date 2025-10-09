using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using EssentialLayers.Request.Helpers.Estension;
using EssentialLayers.Request.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Helpers.Request
{
	public class RequestHelper
	{
		private readonly HttpClient HttpClient;

		private readonly HttpOption HttpOption;

		/**/

		public RequestHelper(IHttpClientFactory httpClientFactory)
		{
			HttpClient = httpClientFactory.CreateClient();

			HttpOption = new()
			{
				AppName = "AppName",
				AppVersion = "1.0"
			};
		}

		public async Task<HttpResponseMessage?> SendAsync<TRequest>(
			TRequest request, string url, HttpMethod httpMethod,
			RequestOptions? options
		)
		{
			try
			{
				RequestOptions requestOptions = options ?? new()
				{
					ResultType = HttpOption.ResultType,
					InsensitiveMapping = HttpOption.InsensitiveMapping,
					BearerToken = HttpOption.BearerToken,
					BaseUri = HttpOption.BaseUri
				};

				HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
					$"{HttpOption.AppName}/{HttpOption.AppVersion}"
				);

				foreach (KeyValuePair<string, string> header in requestOptions.Headers!)
				{
					HttpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
				}

				string jsonRequest = request.Serialize();
				string bearerToken = requestOptions.BearerToken.NotEmpty() ? requestOptions.BearerToken : string.Empty;

				ResultHelper<HttpContent> contentResult = request.ToHttpContent(requestOptions.ContentType);

				string uri = url;
				
				GlobalFunctions.Info(url, httpMethod.Method, jsonRequest);

				if (HttpClient.BaseAddress.NotNull()) uri = $"{HttpClient.BaseAddress!.AbsoluteUri}{url}";

				using HttpRequestMessage httpRequestMessage = new()
				{
					Content = contentResult.Data,
					RequestUri = new Uri(uri),
					Method = httpMethod
				};

				httpRequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(requestOptions.ContentType);
				httpRequestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(requestOptions.ContentType));

				if (bearerToken.NotEmpty())
				{
					httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(
						"Bearer", bearerToken
					);
				}

				HttpResponseMessage? httpResponseMessage = null;

				if (requestOptions.IsCached)
				{
					string key = request.Serialize();

					httpResponseMessage = await CacheHelper<HttpResponseMessage>.HttpResponseMessage.GetOrCreate(
						key, async () => await HttpClient.SendAsync(
							httpRequestMessage, requestOptions.CancellationToken
						)
					);
				}
				else
				{
					httpResponseMessage = await HttpClient.SendAsync(
						httpRequestMessage, requestOptions.CancellationToken
					);
				}

				return httpResponseMessage;
			}
			catch (Exception e)
			{
				GlobalFunctions.Error(e);

				return null!;
			}
		}

		public void SetOptions(HttpOption httpOption)
		{
			HttpOption.AppName = httpOption.AppName;
			HttpOption.AppVersion = httpOption.AppVersion;
			HttpOption.BaseUri = httpOption.BaseUri;
			HttpOption.BearerToken = httpOption.BearerToken;
			HttpOption.ResultType = httpOption.ResultType;
			HttpOption.InsensitiveMapping = httpOption.InsensitiveMapping;

			HttpClient.BaseAddress = new Uri(httpOption.BaseUri);
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Bearer", httpOption.BearerToken
			);
		}
	}
}