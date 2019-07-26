using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public static class MvcBuilderExtensions
	{
		public static IMvcCoreBuilder AddRouting(
			this IMvcCoreBuilder builder,
			Action<IRoutingBuilder> routing)
		{
			builder.Services.AddRouting(routing);

			return builder;
		}

		public static IMvcBuilder AddRouting(
			this IMvcBuilder builder,
			Action<IRoutingBuilder> routing)
		{
			builder.Services.AddRouting(routing);

			return builder;
		}

		public static IServiceCollection AddRouting(
			this IServiceCollection services,
			Action<IRoutingBuilder> routing)
		{
			routing(new RoutingBuilder(services));

			return services.AddSingleton<IPostConfigureOptions<MvcOptions>, RoutingPostConfigureOptions>();
		}
	}
}