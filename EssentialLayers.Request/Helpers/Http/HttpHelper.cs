using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using EssentialLayers.Request.Helpers.Estension;
using EssentialLayers.Request.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Helpers.Http
{
	public class HttpHelper
	{
		private readonly HttpClient HttpClient;

		private readonly HttpOption HttpOption;

		/**/

		public HttpHelper(IHttpClientFactory httpClientFactory)
		{
			HttpClient = httpClientFactory.CreateClient();

			HttpOption = new()
			{
				AppName = "AppName",
				AppVersion = "1.0"
			};
		}

		public async Task<HttpResponse<TResult>> SendAsync<TResult, TRequest>(
			TRequest? request, string url, HttpMethod httpMethod,
			RequestOptions? options
		)
		{
			try
			{
				options ??= new RequestOptions();
				bool insensitiveMapping = true;

				AddHeaders(HttpClient, options);

				if (options.BaseUri.NotEmpty()) HttpClient.BaseAddress = new Uri(options.BaseUri);

				if (false.IsAny(options.InsensitiveMapping, HttpOption.InsensitiveMapping)) insensitiveMapping = false;

				string uri = url;

				if (HttpClient.BaseAddress.NotNull()) uri = $"{HttpClient.BaseAddress!.AbsoluteUri}{url}";

				using HttpRequestMessage httpRequestMessage = new()
				{
					RequestUri = new Uri(uri),
					Method = httpMethod
				};

				if (request.NotNull())
				{
					string jsonRequest = request.Serialize();
					ResultHelper<HttpContent> contentResult = request.ToHttpContent(options.ContentType);

					if (contentResult.Ok.False()) return HttpResponse<TResult>.Fail(
						contentResult.Message, HttpStatusCode.InternalServerError
					);

					GlobalFunctions.Info(url, $"Request - {httpMethod.Method}", jsonRequest);

					httpRequestMessage.Content = contentResult.Data;

					httpRequestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(options.ContentType);
				}

				httpRequestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(options.ContentType));

				AddToken(httpRequestMessage, options);

				HttpResponseMessage? httpResponseMessage = null;

				if (options.IsCached)
				{
					string key = request.Serialize();

					httpResponseMessage = await CacheHelper<TResult>.HttpResponseMessage.GetOrCreate(
						key, async () => await HttpClient.SendAsync(
							httpRequestMessage, options.CancellationToken
						)
					);
				}
				else
				{
					httpResponseMessage = await HttpClient.SendAsync(
						httpRequestMessage, options.CancellationToken
					);
				}

				string response = await httpResponseMessage.Content.ReadAsStringAsync();

				GlobalFunctions.Info(url, $"Result - {httpMethod.Method}", response);

				if (httpResponseMessage.IsSuccessStatusCode.False()) return HttpResponse<TResult>.Fail(
					response, httpResponseMessage.StatusCode
				);

				return ManageResponse<TResult>(
					httpResponseMessage.StatusCode, response, HttpOption.CastResultAsResultHelper, insensitiveMapping
				);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		private void AddHeaders(
			HttpClient httpClient, RequestOptions options
		)
		{
			httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
					$"{HttpOption.AppName}/{HttpOption.AppVersion}"
			);

			foreach (KeyValuePair<string, string> header in options.Headers)
			{
				httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
			}
		}

		private void AddToken(HttpRequestMessage httpRequestMessage, RequestOptions options)
		{
			string bearerToken = options.BearerToken.NotEmpty() ? options.BearerToken : string.Empty;

			if (bearerToken.IsEmpty()) bearerToken = HttpOption.BearerToken;

			if (bearerToken.NotEmpty())
			{
				httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(
					"Bearer", bearerToken
				);
			}
		}

		public HttpResponse<TResult> ManageResponse<TResult>(
			HttpStatusCode httpStatusCode, string response, bool castAsResultHelper, bool insensitiveMapping
		)
		{
			switch (httpStatusCode)
			{
				case HttpStatusCode.OK:
				case HttpStatusCode.BadRequest:
				case HttpStatusCode.InternalServerError:

					if (castAsResultHelper)
					{
						ResultHelper<TResult> resultHelper = response.Deserialize<ResultHelper<TResult>>(insensitive: insensitiveMapping);

						if (resultHelper.Ok.False()) return HttpResponse<TResult>.Fail(
							resultHelper.Message, httpStatusCode
						);

						return HttpResponse<TResult>.Success(resultHelper.Data, httpStatusCode);
					}
					else
					{
						TResult? result = response.Deserialize<TResult>(insensitive: insensitiveMapping);

						return HttpResponse<TResult>.Success(result, httpStatusCode);
					}

				case HttpStatusCode.NotFound:

					return HttpResponse<TResult>.Fail(
						"The request method not found", httpStatusCode
					);

				case HttpStatusCode.ServiceUnavailable:

					return HttpResponse<TResult>.Fail(
						"The server is unavailable", httpStatusCode
					);

				case HttpStatusCode.Forbidden:

					return HttpResponse<TResult>.Fail(
						"The request is forbidden", httpStatusCode
					);

				default:

					return HttpResponse<TResult>.Fail(response.Serialize(), httpStatusCode);
			}
		}

		public void SetOptions(HttpOption httpOption)
		{
			HttpOption.AppName = httpOption.AppName;
			HttpOption.AppVersion = httpOption.AppVersion;
			HttpOption.BaseUri = httpOption.BaseUri;
			HttpOption.BearerToken = httpOption.BearerToken;
			HttpOption.CastResultAsResultHelper = httpOption.CastResultAsResultHelper;
			HttpOption.InsensitiveMapping = httpOption.InsensitiveMapping;

			HttpClient.BaseAddress = new Uri(httpOption.BaseUri);
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Bearer", httpOption.BearerToken
			);
		}
	}
}