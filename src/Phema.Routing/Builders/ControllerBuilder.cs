using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public interface IControllerBuilder<TController>
	{
		IControllerActionRouteBuilder AddRoute<TResult>(
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

		public IControllerActionRouteBuilder AddRoute<TResult>(
			string template,
			Expression<Func<TController, TResult>> expression)
		{
			var declaration = new RouteDeclaration(template);

			services.Configure<RoutingOptions>(options =>
			{
				var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

				foreach (var (info, parameter) in RouteBindingSourceHelper.GetRouteParameters(methodCall))
				{
					options.Parameters.Add(info, parameter);
				}

				options.Actions.Add(methodCall.Method, declaration);
			});

			return new ControllerActionRouteBuilder(declaration);
		}
	}
}