using EssentialLayers.Request.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Services.Factory
{
	public interface IHttpFactory
	{
		void Set(string clientName);

		Task<HttpResponse<TResult>> GetAsync<TResult>(string url);

		Task<HttpResponse<Stream>> GetStreamAsync(string url);

		Task<HttpResponse<byte[]>> GetBytesAsync(string url);

		Task<HttpResponse<TResult>> PostAsync<TResult, TRequest>(string url, TRequest request);

		Task<HttpResponse<TResult>> PutAsync<TResult, TRequest>(string url, TRequest request);

		Task<HttpResponse<TResult>> DeleteAsync<TResult>(string url);
	}
}