using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public interface IControllerBuilder<TController>
	{
		IRouteBuilder AddRoute<TResult>(
			string template, 
			Expression<Func<TController, TResult>> expression);
	}
	
	internal sealed class ControllerBuilder<TController> : IControllerBuilder<TController>
	{
		private readonly IServiceCollection services;

		public ControllerBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		public IRouteBuilder AddRoute<TResult>(
			string template, 
			Expression<Func<TController, TResult>> expression)
		{
			var metadata = new RouteMetadata(template);

			services.Configure<RoutingOptions>(options =>
			{
				var method = RouteHelper.AddRouteParameters(options.Parameters, expression);

				options.Actions.Add(method, metadata);
			});

			return new RouteBuilder(metadata);
		}
	}
}