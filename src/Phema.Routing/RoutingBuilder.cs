using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	internal sealed class RoutingBuilder : IRoutingBuilder
	{
		private readonly IServiceCollection services;

		public RoutingBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		public IRouteBuilder AddController<TController>(
			string template,
			Action<IControllerBuilder<TController>> controller)
		{
			var metadata = new RouteMetadata(template);

			services.Configure<PhemaRoutingConfigurationOptions>(options =>
				options.Controllers.Add(typeof(TController).GetTypeInfo(), metadata));

			controller(new ControllerBuilder<TController>(services));

			return new RouteBuilder(metadata);
		}
	}
}