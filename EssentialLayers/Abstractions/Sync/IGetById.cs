namespace EssentialLayers.Abstractions.Sync
{
	public interface IGetById<T> where T : class
	{
		T GetById(int id);
	}
}