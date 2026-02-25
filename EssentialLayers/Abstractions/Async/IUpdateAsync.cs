using EssentialLayers.Helpers.Result;
using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface IUpdateAsync<T> where T : class
	{
		Task<ResultHelper<T>> UpdateAsync(T value);
	}
}