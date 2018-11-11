using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public static class RoutingExtensions
	{
		public static IMvcCoreBuilder AddRouting(this IMvcCoreBuilder builder, Action<IRoutingBuilder> action)
		{
			AddRoutingCore(builder.Services, action);

			return builder;
		}

		public static IMvcBuilder AddRouting(this IMvcBuilder builder, Action<IRoutingBuilder> action)
		{
			AddRoutingCore(builder.Services, action);

			return builder;
		}

		private static void AddRoutingCore(IServiceCollection services, Action<IRoutingBuilder> action)
		{
			if (!services.Any(x => x.ImplementationType == typeof(MvcOptionsPostConfiguration)))
			{
				services.AddSingleton<IPostConfigureOptions<MvcOptions>, MvcOptionsPostConfiguration>();
			}

			action(new RoutingBuilder(services));
		}
	}
}