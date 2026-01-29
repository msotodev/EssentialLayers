using EssentialLayers.Helpers.Extension;
using EssentialLayers.Request.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Services.Factory
{
	internal class HttpFactory(
		IHttpClientFactory httpClientFactory,
		ILogger<HttpFactory> logger,
		IFactoryTokenProvider tokenProvider
	) : IHttpFactory
	{
		public async Task<HttpResponse<TResult>> GetAsync<TResult>(string clientName, string url)
		{
			try
			{
				HttpClient httpClient = GetHttpClient(clientName);

				if (httpClient == null) throw new(nameof(httpClient));

				string absoluteUri = httpClient.BaseAddress?.AbsoluteUri ?? string.Empty;

				InfoRequest(absoluteUri, url, string.Empty);

				AddAuth(httpClient);

				HttpResponseMessage result = await httpClient.GetAsync(url);

				return await ManageResponse<TResult>(absoluteUri, url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<Stream>> GetStreamAsync(string clientName, string url)
		{
			try
			{
				HttpClient httpClient = GetHttpClient(clientName);

				if (httpClient == null) throw new(nameof(httpClient));

				string absoluteUri = httpClient.BaseAddress?.AbsoluteUri ?? string.Empty;

				InfoRequest(absoluteUri, url, string.Empty);

				AddAuth(httpClient);

				Stream result = await httpClient.GetStreamAsync(url);

				InfoResult(absoluteUri, url, $"Size: {result.Length}");

				return HttpResponse<Stream>.Success(result, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return HttpResponse<Stream>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<byte[]>> GetBytesAsync(string clientName, string url)
		{
			try
			{
				HttpClient httpClient = GetHttpClient(clientName);

				if (httpClient == null) throw new(nameof(httpClient));

				string absoluteUri = httpClient.BaseAddress?.AbsoluteUri ?? string.Empty;

				InfoRequest(absoluteUri, url, string.Empty);

				AddAuth(httpClient);

				byte[] result = await httpClient.GetByteArrayAsync(url);

				InfoResult(absoluteUri, url, $"Size: {result.Length}");

				return HttpResponse<byte[]>.Success(result, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return HttpResponse<byte[]>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<TResult>> PostAsync<TResult, TRequest>(string clientName, string url, TRequest request)
		{
			try
			{
				HttpClient httpClient = GetHttpClient(clientName);

				if (httpClient == null) throw new(nameof(httpClient));

				string absoluteUri = httpClient.BaseAddress?.AbsoluteUri ?? string.Empty;
				string serialized = request.Serialize();

				InfoRequest(absoluteUri, url, serialized);

				AddAuth(httpClient);

				HttpResponseMessage result = await httpClient.PostAsync(url, GetContent(httpClient, serialized));

				return await ManageResponse<TResult>(absoluteUri, url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<TResult>> PutAsync<TResult, TRequest>(string clientName, string url, TRequest request)
		{
			try
			{
				HttpClient httpClient = GetHttpClient(clientName);

				if (httpClient == null) throw new(nameof(httpClient));

				string absoluteUri = httpClient.BaseAddress?.AbsoluteUri ?? string.Empty;
				string serialized = request.Serialize();

				InfoRequest(absoluteUri, url, serialized);

				AddAuth(httpClient);

				HttpResponseMessage result = await httpClient.PutAsync(url, GetContent(httpClient, serialized));

				return await ManageResponse<TResult>(absoluteUri, url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<TResult>> DeleteAsync<TResult>(string clientName, string url)
		{
			try
			{
				HttpClient httpClient = GetHttpClient(clientName);

				if (httpClient == null) throw new(nameof(httpClient));

				string absoluteUri = httpClient.BaseAddress?.AbsoluteUri ?? string.Empty;

				InfoRequest(absoluteUri, url, string.Empty);

				AddAuth(httpClient);

				HttpResponseMessage result = await httpClient.DeleteAsync(url);

				return await ManageResponse<TResult>(absoluteUri, url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		private void AddAuth(HttpClient httpClient)
		{
			string token = tokenProvider.GetToken();

			if (token.NotEmpty())
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}

			string apiKey = tokenProvider.GetApiKey();

			if (httpClient != null && apiKey.NotEmpty())
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", apiKey);
			}

			string headerApiKey = tokenProvider.GetHeaderApiKey();

			if (httpClient != null && headerApiKey.NotEmpty())
			{
				httpClient.DefaultRequestHeaders.Add("X-Api-Key", headerApiKey);
			}
		}

		private static string GetFullUrl(string absoluteUri, string url) => $"{absoluteUri}{url}";

		private void InfoRequest(string absoluteUri, string url, string request)
		{
			string fullUrl = GetFullUrl(absoluteUri, url);

			logger.LogInformation(
				"[ Url: {fullUrl} - Request: {request} ]", fullUrl, request
			);
		}

		private void InfoResult(string absoluteUri, string url, string result)
		{
			string fullUrl = GetFullUrl(absoluteUri, url);

			logger.LogInformation(
				"[ Url: {fullUrl} - Result: {result} ]", fullUrl, result
			);
		}

		private async Task<HttpResponse<TResult>> ManageResponse<TResult>(
			string absoluteUri, string url, HttpResponseMessage result
		)
		{
			if (result.IsSuccessStatusCode.False())
			{
				string errorMessage = await result.Content.ReadAsStringAsync();

				logger.LogWarning(
					"BadRequest: {status} - {message}", result.StatusCode, errorMessage
				);

				return HttpResponse<TResult>.Fail($"[{result.StatusCode}] {errorMessage}", result.StatusCode);
			}

			string stringResult = await result.Content.ReadAsStringAsync();

			InfoResult(absoluteUri, url, stringResult);

			TResult? deserialized = stringResult.Deserialize<TResult>();

			return HttpResponse<TResult>.Success(deserialized, HttpStatusCode.OK);
		}

		private static StringContent GetContent(
			HttpClient httpClient, string json) => new(json, Encoding.UTF8, GetDefaultContentType(httpClient)
		);

		private static string GetDefaultContentType(HttpClient httpClient)
		{
			string? mediaType = httpClient?.DefaultRequestHeaders?.Accept?.FirstOrDefault()?.MediaType;

			return mediaType ?? "application/json";
		}

		private HttpClient GetHttpClient(string clientName)
		{
			HttpClient httpClient = httpClientFactory.CreateClient(clientName);

			return httpClient;
		}
	}
}