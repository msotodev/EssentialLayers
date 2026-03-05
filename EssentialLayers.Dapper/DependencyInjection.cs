using EssentialLayers.Dapper.Abstractions;
using EssentialLayers.Dapper.Factories;
using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Dapper.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EssentialLayers.Dapper
{
	public static class DependencyInjection
	{
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