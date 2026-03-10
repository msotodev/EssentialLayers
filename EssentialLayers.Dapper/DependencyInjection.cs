using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Factories;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Dapper.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EssentialLayers.Dapper
{
	/// <summary>
	/// Provides extension methods for configuring Dapper services in the dependency injection container.
	/// </summary>
	public static class DependencyInjection
	{
		/// <summary>
		/// Adds Dapper services to the service collection.
		/// </summary>
		/// <param name="services">The service collection to add services to.</param>
		/// <returns>The service collection for method chaining.</returns>
		public static IServiceCollection UseDapper(
			this IServiceCollection services
		)
		{
			services.AddOptions<ConnectionOption>().ValidateOnStart();

			services.TryAddSingleton<IDbConnectionFactory, SqlConnectionFactory>();

			services.TryAddScoped<IComplexProcedure, ComplexProcedureService>();
			services.TryAddScoped<IMultipleProcedure, MultipleProcedureService>();
			services.TryAddScoped<INormalProcedure, NormalProcedureService>();

			services.TryAddScoped<IQueryService, QueryService>();

			return services;
		}
	}
}