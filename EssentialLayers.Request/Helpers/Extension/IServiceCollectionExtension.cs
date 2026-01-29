using EssentialLayers.Request.Services.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace EssentialLayers.Request.Helpers.Extension
{
	public static class IServiceCollectionExtension
	{
		public static void AddHttpClients(
			this IServiceCollection services, IConfiguration configuration
		)
		{
			services.AddTransient<AuthHeaderHandler>();

			IConfigurationSection clients = configuration.GetSection($"HttpClients");

			foreach (IConfigurationSection section in clients.GetChildren())
			{
				string? clientName = section.Key;

				if (clientName == null) continue;

				string? baseUrl = section["BaseUrl"];
				string? userAgent = section["UserAgent"];
				string? contentType = section["ContentType"];

				userAgent ??= "MyApp/1.0";
				contentType ??= "application/json";

				if (baseUrl != null)
				{
					services.AddHttpClient(
						clientName, (client) =>
						{
							client.BaseAddress = new Uri(baseUrl);
							client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
							client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
						}
					).AddHttpMessageHandler<AuthHeaderHandler>();
				}
			}
		}
	}
}