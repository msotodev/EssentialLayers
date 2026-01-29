using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Dapper.Options;
using EssentialLayers.Dapper.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EssentialLayers.Dapper
{
	public static class Startup
	{
		public static IServiceCollection UseDapper(
			this IServiceCollection services
		)
		{
			services.AddOptions<ConnectionOption>().ValidateOnStart();

			services.TryAddScoped<ProcedureService>();
			services.TryAddScoped<IComplexProcedure>(sp => sp.GetRequiredService<ProcedureService>());
			services.TryAddScoped<INormalProcedure>(sp => sp.GetRequiredService<ProcedureService>());
			services.TryAddScoped<IMultipleProcedure>(sp => sp.GetRequiredService<ProcedureService>());

			services.TryAddScoped<IQueryService, QueryService>();

			return services;
		}
	}
}