using Common.Dtos;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController(ILogger<UserController> logger) : ControllerBase
	{
		private static readonly IList<UserResponseDto> Users = Enumerable.Range(1, 10).Select(
			index => new UserResponseDto
			{
				Id = index,
				Mail = $"name{index}@mail.com",
				Age = Random.Shared.Next(18, 55),
				UserName = $"Name{index}"
			}
		).ToList();

		[HttpGet("All")]
		public IList<UserResponseDto> GetAll()
		{
			return Users;
		}

		[HttpGet("ById/{id}")]
		public ResultHelper<UserResponseDto> Get(int id)
		{
			UserResponseDto? first = Users.FirstOrDefault(user => user.Id == id);

			if (first.IsNull()) return ResultHelper<UserResponseDto>.Fail("The user doesn't exists");

			return ResultHelper<UserResponseDto>.Success(first!);
		}

		[HttpGet("NameById/{id}")]
		public string GetName(int id)
		{
			UserResponseDto? first = Users.FirstOrDefault(user => user.Id == id);

			if (first.IsNull()) return string.Empty;

			return first!.UserName;
		}
	}
}