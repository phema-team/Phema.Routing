using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public interface IRoutingBuilder
	{
		IRouteBuilder AddController<TController>(string template, Action<IControllerBuilder<TController>> controller);
	}
	
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
			var declaration = new RouteDeclaration(template);

			services.Configure<RoutingOptions>(options =>
				options.Controllers.Add(typeof(TController).GetTypeInfo(), declaration));

			controller(new ControllerBuilder<TController>(services));

			return new RouteBuilder(declaration);
		}
	}
}