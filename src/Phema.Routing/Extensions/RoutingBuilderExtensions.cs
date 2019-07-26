using System;
using System.Linq.Expressions;

namespace Phema.Routing
{
	public static class RoutingBuilderExtensions
	{
		public static IControllerRouteBuilder AddController<TController>(
			this IRoutingBuilder builder,
			Action<IControllerBuilder<TController>> action)
		{
			return builder.AddController(RoutingDefaults.DefaultTemplate, action);
		}

		public static IActionRouteBuilder AddRoute<TController, TResult>(
			this IControllerBuilder<TController> builder,
			Expression<Func<TController, TResult>> expression)
		{
			return builder.AddRoute(RoutingDefaults.DefaultTemplate, expression);
		}
	}
}