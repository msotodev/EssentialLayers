using EssentialLayers.Helpers.Result;
using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface INewAsync<T> where T : class
	{
		Task<ResultHelper<T>> NewAsync(T value);
	}
}