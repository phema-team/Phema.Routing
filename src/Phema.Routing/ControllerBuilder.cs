using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
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

			services.Configure<PhemaConfigurationOptions>(options =>
			{
				var method = RouteHelper.AddRouteParameters(options.Parameters, expression);

				options.Actions.Add(method, metadata);
			});

			return new RouteBuilder(metadata);
		}
	}
}