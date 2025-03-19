using EssentialLayers.Helpers.Extension;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace EssentialLayers.Helpers.Result
{
	public class ResultHelper<T>(bool ok, string message, T data)
	{
		public bool Ok { get; set; } = ok;

		public string Message { get; set; } = message;

		public T Data { get; set; } = data;

		/**/

		public static ResultHelper<T> Success(T data) => new(true, string.Empty, data);

		public static ResultHelper<T> Fail(string message) => new(false, message, default!);

		public static ResultHelper<T> Fail(
			Exception e,
			[CallerFilePath] string file = null,
			[CallerMemberName] string member = null,
			[CallerLineNumber] int lineNumber = 0
		)
		{
			if (e.InnerException is JsonSerializationException jsonEx) return Fail(
				$"Error de deserialización en la ruta '{jsonEx.Path}': {jsonEx.Message}"
			);

			Type type = e.InnerException?.GetType();

			if (type != null && ErrorMessages.Messages.TryGetValue(type, out string message) && message.NotNull())
			{
				return new ResultHelper<T>(
					false, message, default!
				);
			}

			return Fail(e.Message);
		}
	}
}