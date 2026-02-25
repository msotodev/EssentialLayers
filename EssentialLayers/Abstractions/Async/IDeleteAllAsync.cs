using EssentialLayers.Helpers.Result;
using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface IDeleteAllAsync<T> where T : class
	{
		Task<Response> DeleteAll();
	}
}