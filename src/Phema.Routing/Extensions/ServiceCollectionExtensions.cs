using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public static class RoutingExtensions
	{
		public static IMvcCoreBuilder AddPhemaRouting(this IMvcCoreBuilder builder, Action<IRoutingBuilder> action)
		{
			AddPhemaRoutingCore(builder.Services, action);

			return builder;
		}

		public static IMvcBuilder AddPhemaRouting(this IMvcBuilder builder, Action<IRoutingBuilder> action)
		{
			AddPhemaRoutingCore(builder.Services, action);

			return builder;
		}

		private static void AddPhemaRoutingCore(IServiceCollection services, Action<IRoutingBuilder> action)
		{
			if (!services.Any(x => x.ImplementationType == typeof(PhemaRoutingMvcOptionsPostConfiguration)))
			{
				services.AddSingleton<IPostConfigureOptions<MvcOptions>, PhemaRoutingMvcOptionsPostConfiguration>();
			}

			action(new RoutingBuilder(services));
		}
	}
}