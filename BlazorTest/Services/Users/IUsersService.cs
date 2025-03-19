using Common.Dtos;
using EssentialLayers.Helpers.Result;

namespace BlazorTest.Services.Users
{
	public interface IUsersService
	{
		Task<ResultHelper<UserResponseDto>> GetAsync(int id);

		Task<ResultHelper<IList<UserResponseDto>>> GetAllAsync();
		
		Task<string> GetNameAsync(int id);
	}
}