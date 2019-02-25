using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public static class RoutingExtensions
	{
		public static IMvcCoreBuilder AddPhemaRouting(
			this IMvcCoreBuilder builder,
			Action<IRoutingBuilder> routing)
		{
			AddPhemaRoutingCore(builder.Services, routing);

			return builder;
		}

		public static IMvcBuilder AddPhemaRouting(
			this IMvcBuilder builder,
			Action<IRoutingBuilder> routing)
		{
			AddPhemaRoutingCore(builder.Services, routing);

			return builder;
		}

		private static void AddPhemaRoutingCore(IServiceCollection services, Action<IRoutingBuilder> routing)
		{
			if (!services.Any(x => x.ImplementationType == typeof(PhemaMvcOptionsPostConfiguration)))
			{
				services.AddSingleton<IPostConfigureOptions<MvcOptions>, PhemaMvcOptionsPostConfiguration>();
			}

			routing(new RoutingBuilder(services));
		}
	}
}