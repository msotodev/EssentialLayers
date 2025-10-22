using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Services.Factory
{
	internal class HttpFactory(
		IHttpClientFactory httpClientFactory,
		IOptions<HttpFactoryOptions> factoryOptions,
		ILogger<HttpFactory> logger,
		IFactoryTokenProvider tokenProvider
	) : IHttpFactory
	{
		private readonly HttpClient httpClient = httpClientFactory.CreateClient(factoryOptions.Value.ClientName);

		public async Task<ResultHelper<TResult>> GetAsync<TResult>(string url)
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
				return ResultHelper<TResult>.Fail(e);
			}
		}

		public async Task<ResultHelper<Stream>> GetStreamAsync(string url)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				InfoRequest(url, string.Empty);

				AddBearerAuth();

				Stream result = await httpClient.GetStreamAsync(url);

				InfoResult(url, $"Size: {result.Length}");

				return ResultHelper<Stream>.Success(result);
			}
			catch (Exception e)
			{
				return ResultHelper<Stream>.Fail(e);
			}
		}

		public async Task<ResultHelper<byte[]>> GetBytesAsync(string url)
		{
			try
			{
				if (httpClient == null) throw new(nameof(httpClient));

				InfoRequest(url, string.Empty);

				AddBearerAuth();

				byte[] result = await httpClient.GetByteArrayAsync(url);

				InfoResult(url, $"Size: {result.Length}");

				return ResultHelper<byte[]>.Success(result);
			}
			catch (Exception e)
			{
				return ResultHelper<byte[]>.Fail(e);
			}
		}

		public async Task<ResultHelper<TResult>> PostAsync<TResult, TRequest>(string url, TRequest request)
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
				return ResultHelper<TResult>.Fail(e);
			}
		}

		public async Task<ResultHelper<TResult>> PutAsync<TResult, TRequest>(string url, TRequest request)
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
				return ResultHelper<TResult>.Fail(e);
			}
		}

		public async Task<ResultHelper<TResult>> DeleteAsync<TResult>(string url)
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
				return ResultHelper<TResult>.Fail(e);
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

		private async Task<ResultHelper<TResult>> ManageResponse<TResult>(
			string url, HttpResponseMessage result
		)
		{
			if (result.IsSuccessStatusCode.False())
			{
				string errorMessage = await result.Content.ReadAsStringAsync();

				logger.LogWarning(
					"BadRequest: {status} - {message}", result.StatusCode, errorMessage
				);

				return ResultHelper<TResult>.Fail($"[{result.StatusCode}] {errorMessage}");
			}

			string stringResult = await result.Content.ReadAsStringAsync();

			InfoResult(url, stringResult);

			TResult? deserialized = stringResult.Deserialize<TResult>();

			return ResultHelper<TResult>.Success(deserialized);
		}

		private StringContent GetContent(string json) => new(json, Encoding.UTF8, factoryOptions.Value.DefaultContentType);
	}
}