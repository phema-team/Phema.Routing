using System;
using System.Linq.Expressions;

namespace Phema.Routing
{
	public static class ControllerBuilderExtensions
	{
		public static IActionRouteBuilder AddRoute<TController, TResult>(
			this IControllerBuilder<TController> builder,
			Expression<Func<TController, TResult>> expression)
		{
			return builder.AddRoute(RoutingDefaults.DefaultTemplate, expression);
		}
	}
}