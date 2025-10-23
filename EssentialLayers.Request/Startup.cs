using EssentialLayers.Request.Models;
using EssentialLayers.Request.Services.Factory;
using EssentialLayers.Request.Services.Http;
using EssentialLayers.Request.Services.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http.Headers;

namespace EssentialLayers.Request
{
	public static class Startup
	{
		public static IServiceCollection UseRequest(
			this IServiceCollection services
		)
		{
			services.AddHttpClient();
			services.TryAddSingleton<IHttpService, HttpService>();
			services.TryAddSingleton<IRequestService, RequestService>();

			return services;
		}

		public static IServiceProvider ConfigureRequest(
			this IServiceProvider provider, HttpOption httpOption
		)
		{
			IHttpService httpService = provider.GetRequiredService<IHttpService>();
			IRequestService requestService = provider.GetRequiredService<IRequestService>();

			httpService.SetOptions(httpOption);
			requestService.SetOptions(httpOption);

			return provider;
		}

		//
		// Summary:
		//     To use this service you should've configured in your appsettings.json the section HttpClients
		//	   
		//     "HttpClients": {
		//			"FirstApiClient": {
		//				"ClientName": "FirstApiClient",
		//				"BaseUrl": "https://localhost:5000/api/",
		//				"UserAgent": "FirstApiClient/1.0", (Optional) => Default 'MyApp/1.0'
		//				"ContentType": "application/json" (Optional) => Default 'application/json'
		//			}
		//			"SecondApiClient": {
		//				"ClientName": "SecondApiClient",
		//				"BaseUrl": "https://localhost:5001/api/",
		//				"UserAgent": "SecondApiClient/1.0", (Optional) => Default 'MyApp/1.0'
		//				"ContentType": "application/json" (Optional) => Default 'application/json'
		//			}
		//		}
		//
		// Parameters:
		//   services:
		//     Service collection to which to add authentication.
		//
		//   configuration:
		//     The Configuration object.
		//
		// Returns:
		//     Configuration to use IHttpFactory interface
		public static ServiceCollection ConfigureFactory(
			this ServiceCollection services, IConfiguration configuration
		)
		{
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
					);
				}
			}

			services.AddScoped<IFactoryTokenProvider, FactoryTokenProvider>();
			services.AddScoped<IHttpFactory, HttpFactory>();

			return services;
		}
	}
}