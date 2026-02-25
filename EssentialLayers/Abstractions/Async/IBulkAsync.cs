using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface IBulkAsync<T> where T : class
	{
		Task<Response> BulkAsync(List<T> values);
	}
}