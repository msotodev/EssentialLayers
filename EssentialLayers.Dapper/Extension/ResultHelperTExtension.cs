using EssentialLayers.Helpers.Result;
using System;

namespace EssentialLayers.Dapper.Extension
{
	public static class ResultHelperTExtension
	{
		public static ResultHelper<T> HandleException<T>(this Exception exception)
		{
			if (exception.Message.Contains(
				"A parameterless default constructor or one matching signature")
			) return ResultHelper<T>.Fail(
				"Check the order, name and datatype of the properties in the DTO and the query result set."
			);

			return ResultHelper<T>.Fail(exception.Message);
		}
	}
}