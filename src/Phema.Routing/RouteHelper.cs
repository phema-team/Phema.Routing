using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phema.Routing
{
	internal static class RouteHelper
	{
		private static IDictionary<string, BindingSource> BindingSourceMap { get; }

		public static MethodInfo AddRouteParameters<TController, TResult>(
			IDictionary<ParameterInfo, ParameterMetadata> parameters,
			Expression<Func<TController, TResult>> expression)
		{
			var e = (MethodCallExpression)expression.Body;
			AddRouteParameters(parameters, e);
			return e.Method;
		}

		private static void AddRouteParameters(
			IDictionary<ParameterInfo, ParameterMetadata> parameters,
			MethodCallExpression expression)
		{
			var methodParameters = expression.Method.GetParameters();

			for (var i = 0; i < methodParameters.Length; i++)
			{
				var argument = (MethodCallExpression)expression.Arguments[i];
				var methodParameter = methodParameters[i];

				var parameterMetadata = new ParameterMetadata(BindingSourceMap[argument.Method.Name]);
				
				parameters.Add(methodParameter, parameterMetadata);
			}
		}

		static RouteHelper()
		{
			BindingSourceMap = new Dictionary<string, BindingSource>
			{
				[nameof(From.Body)] = BindingSource.Body,
				[nameof(From.Route)] = BindingSource.Path,
				[nameof(From.Header)] = BindingSource.Header,
				[nameof(From.Services)] = BindingSource.Services,
				[nameof(From.Query)] = BindingSource.Query,
				[nameof(From.Form)] = BindingSource.Form,
				[nameof(From.Any)] = BindingSource.Custom,
				[nameof(From.ModelBinding)] = BindingSource.ModelBinding,
				[nameof(From.Special)] = BindingSource.Special,
				[nameof(From.FormFile)] = BindingSource.FormFile
			};
		}
	}
}