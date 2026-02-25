using EssentialLayers.Helpers.Result;
using System.Collections.Generic;

namespace EssentialLayers.Abstractions.Sync
{
	public interface IBulkInsert<T> where T : class
	{
		Response BulkInsert(IReadOnlyList<T> values);
	}
}