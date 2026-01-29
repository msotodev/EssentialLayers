using EssentialLayers.Dapper.Services.Connection;
using EssentialLayers.Dapper.Services.Procedure;
using EssentialLayers.Dapper.Services.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace EssentialLayers.Dapper
{
	public static class Startup
	{
		public static IServiceCollection UseDapper(
			this IServiceCollection services
		)
		{
			services.TryAddSingleton<IConnectionService, ConnectionService>();
			services.TryAddScoped<IProcedureService, ProcedureService>();
			services.TryAddScoped<IQueryService, QueryService>();

			return services;
		}

		public static IServiceProvider ConfigureDapper(
			this IServiceProvider provider, string connectionString
		)
		{
			IConnectionService service = provider.GetRequiredService<IConnectionService>();

			service.Set(connectionString);

			return provider;
		}
	}
}