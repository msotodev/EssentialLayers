using System.Collections.Generic;

namespace EssentialLayers.Abstractions.Sync
{
	public interface IGetAll<T> where T : class
	{
		IReadOnlyList<T> GetAll();
	}
}