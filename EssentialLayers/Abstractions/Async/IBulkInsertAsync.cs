using EssentialLayers.Helpers.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface IBulkInsertAsync<T> where T : class
	{
		Task<Response> BulkInsertAsync(IReadOnlyList<T> values);
	}
}