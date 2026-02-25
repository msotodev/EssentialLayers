using EssentialLayers.Helpers.Result;

namespace EssentialLayers.Abstractions.Sync
{
	public interface IDeleteAll<T> where T : class
	{
		Response DeleteAll();
	}
}