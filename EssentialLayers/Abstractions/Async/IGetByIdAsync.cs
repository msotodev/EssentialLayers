using System.Threading.Tasks;

namespace EssentialLayers.Abstractions.Async
{
	public interface IGetByIdAsync<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
	}
}