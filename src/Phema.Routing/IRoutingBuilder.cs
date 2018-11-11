using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public interface IRoutingBuilder
	{
		IRouteBuilder AddController<TController>(string template, Action<IControllerBuilder<TController>> action);
	}
	
	internal class RoutingBuilder : IRoutingBuilder
	{
		private readonly IServiceCollection services;

		public RoutingBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		public IRouteBuilder AddController<TController>(string template, Action<IControllerBuilder<TController>> action)
		{
			var metadata = new RouteMetadata(template);

			services.Configure<RoutingOptions>(options =>
			{
				options.Controllers.Add(typeof(TController).GetTypeInfo(), metadata);
			});

			action(new ControllerBuilder<TController>(services));

			return new RouteBuilder(metadata);
		}
	}
	
	
}