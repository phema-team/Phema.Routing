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

		public static MethodCallExpression GetMethodCallExpression<TController, TResult>(
			Expression<Func<TController, TResult>> expression)
		{
			return (MethodCallExpression) expression.Body;
		}

		public static IEnumerable<(ParameterInfo info, ParameterDeclaration declaration)> GetRouteParameters(
			MethodCallExpression expression)
		{
			var methodParameters = expression.Method.GetParameters();

			for (var index = 0; index < methodParameters.Length; index++)
			{
				var argument = (MethodCallExpression) expression.Arguments[index];

				var info = methodParameters[index];
				var declaration = new ParameterDeclaration(BindingSourceMap[argument.Method.Name]);

				yield return (info, declaration);
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