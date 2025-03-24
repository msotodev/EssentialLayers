using BlazorTest.Services.Users;
using Common.Dtos;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.AspNetCore.Components;

namespace BlazorTest.Components.Pages
{
	public partial class Home
	{
		[Inject] private IUsersService? UsersService { get; set; }

		/**/

		public string Json { get; set; } = string.Empty;

		public string Endpoint { get; set; } = string.Empty;

		/**/

		private async Task OnClickAll()
		{
			ResultHelper<IList<UserResponseDto>> result = await UsersService!.GetAllAsync();

			Endpoint = "User/All";
			Json = result.Ok ? result.Data.Serialize(true) : string.Empty;
		}

		public async Task OnClickById()
		{
			ResultHelper<UserResponseDto> result = await UsersService!.GetAsync(1);

			Endpoint = $"User/ById/1";
			Json = result.Ok ? result.Data.Serialize(true) : string.Empty;
		}

		public async Task OnClickNameById()
		{
			Endpoint = $"User/NameById/1";
			Json = await UsersService!.GetNameAsync(1);
		}
	}
}