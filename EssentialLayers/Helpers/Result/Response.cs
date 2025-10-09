namespace EssentialLayers.Helpers.Result
{
	public class Response(bool ok, string message)
	{
		public string Message { get; set; } = message;

		public bool Ok { get; set; } = ok;

		/**/

		public Response() : this(true, string.Empty) { }

		public static Response Fail() => new(false, string.Empty);

		public static Response Fail(string message) => new(false, message);

		public static Response Success() => new();

		public static Response Success(string message) => new(true, message);
	}
}