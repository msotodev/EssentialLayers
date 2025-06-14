﻿using EssentialLayers.Helpers.Result;
using System;
using System.Net;

namespace EssentialLayers.Request.Helpers
{
	public class HttpResponse<T>(
		bool ok, string message, T data, HttpStatusCode httpStatusCode = HttpStatusCode.OK
	) : ResultHelper<T>(ok, message, data)
	{
		public HttpStatusCode StatusCode { get; set; } = httpStatusCode;

		public static HttpResponse<T> Success(
			T data, HttpStatusCode httpStatusCode
		) => new(true, string.Empty, data, httpStatusCode);

		public static HttpResponse<T> Fail(
			string message, HttpStatusCode httpStatusCode
		)
		{
			GlobalFunctions.Error(message);

			return new(false, message, default!, httpStatusCode);
		}

		public static HttpResponse<T> Fail(Exception e, HttpStatusCode httpStatusCode)
		{
			ResultHelper<T> result = Fail(e);

			GlobalFunctions.Error(e);

			return Fail(result.Message, httpStatusCode);
		}
	}
}