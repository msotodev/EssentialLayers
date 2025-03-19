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

		/**/

		protected override async Task OnInitializedAsync()
		{
			//ResultHelper<IList<UserResponseDto>> result = await UsersService!.GetAllAsync();

			//if (result.Ok) Json = result.Data.Serialize(true);

			Json = await UsersService!.GetNameAsync(1);
		}
	}
}