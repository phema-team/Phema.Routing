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
			var declaration = new RouteDeclaration(template);

			services.Configure<RoutingOptions>(options =>
			{
				var call = RouteHelper.GetMethodCallExpression(expression);

				foreach (var (info, parameter) in RouteHelper.GetRouteParameters(call))
				{
					options.Parameters.Add(info, parameter);
				}
				
				options.Actions.Add(call.Method, declaration);
			});

			return new RouteBuilder(declaration);
		}
	}
}