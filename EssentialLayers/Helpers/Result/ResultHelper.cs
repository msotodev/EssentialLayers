using System;

namespace EssentialLayers.Helpers.Result
{
	public class ResultHelper<T>(bool ok, string message, T data)
	{
		public T Data { get; set; } = data;

		public string Message { get; set; } = message;

		public bool Ok { get; set; } = ok;

		/**/

		public static ResultHelper<T> Fail(string message) => new(false, message, default!);

		public static ResultHelper<T> Fail(Exception e) => Fail(e.Message);

		public static ResultHelper<T> Success(T data) => new(true, string.Empty, data);

		public static ResultHelper<T> Success(T data, string message) => new(true, message, data);
	}
}