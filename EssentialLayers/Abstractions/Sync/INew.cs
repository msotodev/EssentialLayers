using EssentialLayers.Helpers.Result;

namespace EssentialLayers.Abstractions.Sync
{
	public interface INew<T> where T : class
	{
		ResultHelper<T> New(T value);
	}
}