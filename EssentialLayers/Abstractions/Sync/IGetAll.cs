using System.Collections.Generic;

namespace EssentialLayers.Abstractions.Sync
{
	public interface IGetAll<T> where T : class
	{
		List<T> All { get; }
	}
}