using EssentialLayers.Helpers.Result;

namespace EssentialLayers.Abstractions.Sync
{
	public interface IUpdate<T> where T : class
	{
		ResultHelper<T> Update(T value);
	}
}