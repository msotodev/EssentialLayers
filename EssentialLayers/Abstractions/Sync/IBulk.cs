using EssentialLayers.Helpers.Result;
using System.Collections.Generic;

namespace EssentialLayers.Abstractions.Sync
{
	public interface IBulk<T> where T : class
	{
		Response Bulk(List<T> values);
	}
}