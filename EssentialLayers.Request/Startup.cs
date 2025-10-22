using EssentialLayers.Helpers.Extension;
using EssentialLayers.Request.Models;
using EssentialLayers.Request.Services.Factory;
using EssentialLayers.Request.Services.Http;
using EssentialLayers.Request.Services.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
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
		//				"BaseAddress": "https://localhost:5000/api/",
		//				"UserAgent": "FirstApiClient/1.0"
		//			}
		//			"SecondApiClient": {
		//				"ClientName": "SecondApiClient",
		//				"BaseAddress": "https://localhost:5001/api/",
		//				"UserAgent": "SecondApiClient/1.0"
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
		//   clientName:
		//     The logical name of the client to create
		//
		// Returns:
		//     Configuration to use IHttpFactory interface
		public static IServiceCollection ConfigureFactory(
			this IServiceCollection services, IConfiguration configuration, string clientName
		)
		{
			IConfigurationSection options = configuration.GetSection($"HttpClients:{clientName}");

			if (clientName.NotEmpty())
			{
				services.AddHttpClient(
					clientName, (serviceProvider, client) =>
					{
						IOptions<HttpFactoryOptions>? factoryOptions = serviceProvider.GetService<IOptions<HttpFactoryOptions>>();

						if (factoryOptions != null && factoryOptions.Value != null)
						{
							client.BaseAddress = new Uri(factoryOptions.Value.BaseAddress);
							client.DefaultRequestHeaders.UserAgent.ParseAdd(factoryOptions.Value.UserAgent);
							client.DefaultRequestHeaders.Accept.Add(
								new MediaTypeWithQualityHeaderValue(factoryOptions.Value.DefaultContentType)
							);
						}
					}
				);
			}

			services.AddScoped<IClientFactoryTokenProvider, ClientFactoryTokenProvider>();
			services.AddScoped<IHttpFactory, HttpFactory>();

			return services;
		}
	}
}