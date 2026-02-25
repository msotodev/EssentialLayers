using EssentialLayers.Helpers.Result;
using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface IDeleteAsync<T> where T : class
	{
		Task<Response> DeleteAsync(T value);
	}
}