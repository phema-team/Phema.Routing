using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	public static class UrlHelperExtensions
	{
		/// <summary>
		/// Returns action url from specified expression. 
		/// Use <see cref="From"/> when no matter value is or to get from current request
		/// </summary>
		public static string Action<TController>(
			this IUrlHelper urlHelper,
			Expression<Func<TController, object>> expression)
		{
			var methodCallExpression = RouteHelper.GetInnerMethodCallExpression(expression);
			
			var actionDescriptor = urlHelper.ActionContext
				.HttpContext
				.RequestServices
				.GetRequiredService<IActionDescriptorCollectionProvider>()
				.ActionDescriptors
				.Items
				.OfType<ControllerActionDescriptor>()
				.Single(ad => ad.MethodInfo == methodCallExpression.Method);

			var actionArguments = RouteHelper.GetActionArguments(actionDescriptor.Parameters, methodCallExpression);

			return urlHelper.Action(actionDescriptor.ActionName, actionDescriptor.ControllerName, actionArguments);
		}
	}
}