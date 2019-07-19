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
			AddRouting(builder.Services, routing);

			return builder;
		}

		public static IMvcBuilder AddRouting(
			this IMvcBuilder builder,
			Action<IRoutingBuilder> routing)
		{
			AddRouting(builder.Services, routing);

			return builder;
		}

		private static void AddRouting(IServiceCollection services, Action<IRoutingBuilder> routing)
		{
			services.AddSingleton<IPostConfigureOptions<MvcOptions>, RoutingPostConfigureOptions>();

			routing(new RoutingBuilder(services));
		}
	}
}