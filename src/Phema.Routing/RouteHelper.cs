using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phema.Routing
{
	internal static class RouteHelper
	{
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

				switch (argument.Method.Name)
				{
					case nameof(From.Body):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Body));
						break;

					case nameof(From.Route):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Path));
						break;

					case nameof(From.Header):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Header));
						break;

					case nameof(From.Services):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Services));
						break;

					case nameof(From.Query):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Query));
						break;

					case nameof(From.Form):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Form));
						break;

					case nameof(From.Any):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Custom));
						break;
					
					case nameof(From.ModelBinding):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.ModelBinding));
						break;
					
					case nameof(From.Special):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.Special));
						break;
					
					case nameof(From.FormFile):
						parameters.Add(methodParameters[i], new ParameterMetadata(BindingSource.FormFile));
						break;
					
					default:
						throw new IndexOutOfRangeException(nameof(argument));
				}
			}
		}
	}
}