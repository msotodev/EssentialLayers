using EssentialLayers.Helpers.Result;
using System.IO;
using System.Threading.Tasks;

namespace EssentialLayers.Request.Services.Factory
{
	public interface IHttpFactory
	{
		void Set(string clientName);

		Task<ResultHelper<TResult>> GetAsync<TResult>(string url);

		Task<ResultHelper<Stream>> GetStreamAsync(string url);

		Task<ResultHelper<byte[]>> GetBytesAsync(string url);

		Task<ResultHelper<TResult>> PostAsync<TResult, TRequest>(string url, TRequest request);

		Task<ResultHelper<TResult>> PutAsync<TResult, TRequest>(string url, TRequest request);

		Task<ResultHelper<TResult>> DeleteAsync<TResult>(string url);
	}
}