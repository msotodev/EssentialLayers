using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Services.Http
{
	public class HttpClientFactory(
		IHttpClientFactory httpClientFactory,
		ILogger<HttpClientFactory> logger,
		IClientFactoryTokenProvider tokenProvider,
		string name
	)
	{
		private readonly HttpClient _httpClient = httpClientFactory.CreateClient(name);

		public async Task<ResultHelper<TResult>> GetAsync<TResult>(string url)
		{
			AddBearerAuth();

			string fullUrl = _httpClient.BaseAddress.AbsoluteUri + url;

			InfoRequest(fullUrl, string.Empty);

			try
			{
				string stringResult = await _httpClient.GetStringAsync(url);

				InfoResult(fullUrl, stringResult);

				return ResultHelper<TResult>.Success(stringResult.Deserialize<TResult>());
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
		}

		public async Task<ResultHelper<Stream>> GetStreamAsync(string url)
		{
			AddBearerAuth();

			string fullUrl = _httpClient.BaseAddress.AbsoluteUri + url;

			InfoRequest(fullUrl, string.Empty);

			try
			{
				Stream result = await _httpClient.GetStreamAsync(url);

				InfoResult(fullUrl, $"Size: {result.Length}");

				return ResultHelper<Stream>.Success(result);
			}
			catch (Exception e)
			{
				return ResultHelper<Stream>.Fail(e);
			}
		}

		public async Task<ResultHelper<TResult>> PostAsync<TResult, TRequest>(string url, TRequest request)
		{
			AddBearerAuth();

			string serialized = request.Serialize();

			string fullUrl = _httpClient.BaseAddress.AbsoluteUri + url;

			InfoRequest(fullUrl, serialized);

			try
			{
				StringContent content = new(serialized);

				HttpResponseMessage result = await _httpClient.PostAsync(url, content);

				string stringResult = await result.Content.ReadAsStringAsync();

				InfoResult(fullUrl, stringResult);

				return ResultHelper<TResult>.Success(stringResult.Deserialize<TResult>());
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
		}

		public async Task<ResultHelper<TResult>> PutAsync<TResult, TRequest>(string url, TRequest request)
		{
			AddBearerAuth();

			string serialized = request.Serialize();

			string fullUrl = _httpClient.BaseAddress.AbsoluteUri + url;

			InfoRequest(fullUrl, serialized);

			try
			{
				StringContent content = new(serialized);

				HttpResponseMessage result = await _httpClient.PutAsync(url, content);

				string stringResult = await result.Content.ReadAsStringAsync();

				InfoResult(fullUrl, stringResult);

				return ResultHelper<TResult>.Success(stringResult.Deserialize<TResult>());
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
		}

		public async Task<ResultHelper<TResult>> DeleteAsync<TResult>(string url)
		{
			AddBearerAuth();

			string fullUrl = _httpClient.BaseAddress.AbsoluteUri + url;

			InfoRequest(fullUrl, string.Empty);

			try
			{
				HttpResponseMessage result = await _httpClient.DeleteAsync(url);

				string stringResult = await result.Content.ReadAsStringAsync();

				InfoResult(fullUrl, stringResult);

				return ResultHelper<TResult>.Success(stringResult.Deserialize<TResult>());
			}
			catch (Exception e)
			{
				return ResultHelper<TResult>.Fail(e);
			}
		}

		private void AddBearerAuth()
		{
			string token = tokenProvider.GetToken();

			if (token.NotEmpty())
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
		}

		private void InfoRequest(string url, string request) => logger.LogInformation($"[ Url: {url} - Request: {request} ]");

		private void InfoResult(string url, string result) => logger.LogInformation($"[ Url: {url} - Result: {result} ]");
	}
}