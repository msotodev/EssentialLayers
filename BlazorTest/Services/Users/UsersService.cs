using Common.Dtos;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using EssentialLayers.Request.Helpers;
using EssentialLayers.Request.Models;
using EssentialLayers.Request.Services.Http;
using static EssentialLayers.Request.Helpers.Types.HttpTypes;

namespace BlazorTest.Services.Users
{
	public class UsersService(
		IHttpService httpService
		) : IUsersService
	{
		private const string CONTROLLER_NAME = "User";

		private readonly IHttpService _httpService = httpService;

		/**/

		public async Task<ResultHelper<UserResponseDto>> GetAsync(int id)
		{
			HttpResponse<UserResponseDto> response = await _httpService.GetAsync<UserResponseDto>(
				$"{CONTROLLER_NAME}/ById/{id}", new RequestOptions
				{
					ResultType = ResultType.ResultHelper
				}
			);

			if (response.Ok.False()) return ResultHelper<UserResponseDto>.Fail(response.Message);

			return ResultHelper<UserResponseDto>.Success(response.Data);
		}

		public async Task<ResultHelper<IList<UserResponseDto>>> GetAllAsync()
		{
			HttpResponse<IList<UserResponseDto>> response = await _httpService.GetAsync<IList<UserResponseDto>>(
				$"{CONTROLLER_NAME}/All"
			);

			if (response.Ok.False()) return ResultHelper<IList<UserResponseDto>>.Fail(response.Message);

			return ResultHelper<IList<UserResponseDto>>.Success(response.Data);
		}

		public async Task<string> GetNameAsync(int id)
		{
			HttpResponse<string> response = await _httpService.GetAsync<string>(
				$"{CONTROLLER_NAME}/NameById/{id}", new RequestOptions
				{
					ResultType = ResultType.Object
				}
			);

			if (response.Ok) return response.Data;

			return string.Empty;
		}
	}
}