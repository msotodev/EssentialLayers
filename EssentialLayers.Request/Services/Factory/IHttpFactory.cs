using EssentialLayers.Helpers.Result;
using EssentialLayers.Request.Helpers;
using EssentialLayers.Request.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Services.Factory
{
	public interface IHttpFactory
	{
		Task<HttpResponse<TResult>> GetAsync<TResult>(string clientName, string url);

		Task<HttpResponse<Stream>> GetStreamAsync(string clientName, string url);

		Task<HttpResponse<byte[]>> GetBytesAsync(string clientName, string url);

		Task<HttpResponse<TResult>> PostAsync<TResult, TRequest>(
			string clientName, string url, TRequest request
		);

		Task<Response> PostAsync(
			string clientName, string url, FileRequest fileRequest, IDictionary<string, string>? headers = default
		);

		Task<HttpResponse<TResult>> PatchAsync<TResult, TRequest>(string clientName, string url, TRequest request);

		Task<HttpResponse<TResult>> PatchAsync<TResult>(string clientName, string url);

		Task<Response> PatchAsync(string clientName, string url);

		Task<HttpResponse<TResult>> PutAsync<TResult, TRequest>(string clientName, string url, TRequest request);

		Task<HttpResponse<TResult>> DeleteAsync<TResult>(string clientName, string url);
	}
}