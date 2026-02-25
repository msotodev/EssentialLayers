using EssentialLayers.Helpers.Result;

namespace EssentialLayers.Abstractions.Sync
{
	public interface IDelete<T> where T : class
	{
		Response Delete(T value);
	}
}