using EssentialLayers.Helpers.Result;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EssentialLayers.Helpers.Handlers
{
	public class TryHandler
	{
		public static void Try(Action action)
		{
			try
			{
				action();
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
			}
		}

		public static ResultHelper<T> Try<T>(Func<T> action)
		{
			try
			{
				T result = action();

				return ResultHelper<T>.Success(result);
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);

				return ResultHelper<T>.Fail(e.Message);
			}
		}

		public static async Task TryAsync(Func<Task> action)
		{
			try
			{
				await action();
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
			}
		}

		public static async Task<ResultHelper<T>> TryAsync<T>(Func<Task<T>> action)
		{
			try
			{
				T result = await action();

				return ResultHelper<T>.Success(result);
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);

				return ResultHelper<T>.Fail(e.Message);
			}
		}
	}
}