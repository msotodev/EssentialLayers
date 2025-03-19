namespace Common.Dtos
{
	public class UserResponseDto
	{
		public int Id { get; set; }

		public string UserName { get; set; } = string.Empty;

		public string Mail { get; set; } = string.Empty;

		public int Age { get; set; }
	}
}