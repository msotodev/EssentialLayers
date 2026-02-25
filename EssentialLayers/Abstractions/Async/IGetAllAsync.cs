using System.Collections.Generic;
using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface IGetAllAsync<T> where T : class
	{
		Task<List<T>> All { get; }
	}
}