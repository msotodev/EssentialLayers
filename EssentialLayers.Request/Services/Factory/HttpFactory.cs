using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
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
		private HttpClient? httpClient;

		public void Set(string clientName)
		{
			httpClient = httpClientFactory.CreateClient(clientName);
		}

		public async Task<HttpResponse<TResult>> GetAsync<TResult>(string url)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				InfoRequest(url, string.Empty);

				AddBearerAuth();

				HttpResponseMessage result = await httpClient.GetAsync(url);

				return await ManageResponse<TResult>(url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<Stream>> GetStreamAsync(string url)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				InfoRequest(url, string.Empty);

				AddBearerAuth();

				Stream result = await httpClient.GetStreamAsync(url);

				InfoResult(url, $"Size: {result.Length}");

				return HttpResponse<Stream>.Success(result, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return HttpResponse<Stream>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<byte[]>> GetBytesAsync(string url)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				InfoRequest(url, string.Empty);

				AddBearerAuth();

				byte[] result = await httpClient.GetByteArrayAsync(url);

				InfoResult(url, $"Size: {result.Length}");

				return HttpResponse<byte[]>.Success(result, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return HttpResponse<byte[]>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<TResult>> PostAsync<TResult, TRequest>(string url, TRequest request)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				string serialized = request.Serialize();

				InfoRequest(url, serialized);

				AddBearerAuth();

				HttpResponseMessage result = await httpClient.PostAsync(url, GetContent(serialized));

				return await ManageResponse<TResult>(url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<TResult>> PutAsync<TResult, TRequest>(string url, TRequest request)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				string serialized = request.Serialize();

				InfoRequest(url, serialized);

				AddBearerAuth();

				HttpResponseMessage result = await httpClient.PutAsync(url, GetContent(serialized));

				return await ManageResponse<TResult>(url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		public async Task<HttpResponse<TResult>> DeleteAsync<TResult>(string url)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				InfoRequest(url, string.Empty);

				AddBearerAuth();

				HttpResponseMessage result = await httpClient.DeleteAsync(url);

				return await ManageResponse<TResult>(url, result);
			}
			catch (Exception e)
			{
				return HttpResponse<TResult>.Fail(e, HttpStatusCode.InternalServerError);
			}
		}

		private void AddBearerAuth()
		{
			string token = tokenProvider.GetToken();

			if (httpClient != null && token.NotEmpty())
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
		}

		private string GetFullUrl(string url)
		{
			if (httpClient != null && httpClient.BaseAddress != null) return $"{httpClient.BaseAddress.AbsoluteUri}{url}";

			return string.Empty;
		}

		private void InfoRequest(string url, string request)
		{
			string fullUrl = GetFullUrl(url);

			logger.LogInformation(
				"[ Url: {fullUrl} - Request: {request} ]", fullUrl, request
			);

			Console.WriteLine($"[ Url: {fullUrl} - Request: {request} ]");
		}

		private void InfoResult(string url, string result)
		{
			string fullUrl = GetFullUrl(url);

			logger.LogInformation(
				"[ Url: {fullUrl} - Result: {result} ]", fullUrl, result
			);

			Console.WriteLine($"[ Url: {fullUrl} - Request: {result} ]");
		}

		private async Task<HttpResponse<TResult>> ManageResponse<TResult>(
			string url, HttpResponseMessage result
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

			InfoResult(url, stringResult);

			TResult? deserialized = stringResult.Deserialize<TResult>();

			return HttpResponse<TResult>.Success(deserialized, HttpStatusCode.OK);
		}

		private StringContent GetContent(string json) => new(json, Encoding.UTF8, GetDefaultContentType());

		public string GetDefaultContentType()
		{
			string? mediaType = httpClient?.DefaultRequestHeaders?.Accept?.FirstOrDefault()?.MediaType;

			return mediaType ?? "application/json";
		}
	}
}