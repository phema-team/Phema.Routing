using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phema.Routing
{
	internal static class RouteHelper
	{
		private static IDictionary<string, Func<MethodCallExpression, ParameterDeclaration>> ParameterDeclarationFactoryMap
		{
			get;
		}

		public static MethodCallExpression GetInnerMethodCallExpression<TController, TResult>(
			Expression<Func<TController, TResult>> expression)
		{
			if (expression.Body is MethodCallExpression methodCall)
			{
				return methodCall;
			}

			throw new InvalidOperationException("Controller action method should called first");
		}

		public static IEnumerable<(ParameterInfo info, ParameterDeclaration declaration)> GetRouteParameters(
			MethodCallExpression expression)
		{
			var methodParameters = expression.Method.GetParameters();

			for (var index = 0; index < methodParameters.Length; index++)
			{
				var argument = (MethodCallExpression) expression.Arguments[index];

				if (!ParameterDeclarationFactoryMap.TryGetValue(argument.Method.Name, out var declarationFactory))
				{
					throw new InvalidOperationException(
						$"Use '{nameof(From)}' static class to configure routing binding sources");
				}

				var info = methodParameters[index];
				var declaration = declarationFactory(argument);

				yield return (info, declaration);
			}
		}

		public static Dictionary<string, object> GetActionArguments(
			IEnumerable<ParameterDescriptor> parameterDescriptors,
			MethodCallExpression methodCallExpression)
		{
			var actionParameterNames = parameterDescriptors
				.Select(p => p.BindingInfo.BinderModelName ?? p.Name)
				.ToArray();

			var actionArguments = new Dictionary<string, object>();

			for (var index = 0; index < methodCallExpression.Arguments.Count; index++)
			{
				var argumentExpression = methodCallExpression.Arguments[index];

				if (argumentExpression is MethodCallExpression argumentMethodCallExpression)
				{
					if (argumentMethodCallExpression.Method.DeclaringType != typeof(From))
					{
						throw new InvalidOperationException(
							$"Use '{nameof(From)}' static class or value to configure action argument: '${actionParameterNames[index]}'");
					}
				}

				var argument = FromExpression(argumentExpression);

				if (argument != null)
				{
					actionArguments.Add(actionParameterNames[index], argument);
				}
			}

			return actionArguments;
		}

		private static object FromExpression(Expression expression)
		{
			return expression switch
			{
				ConstantExpression constant => constant.Value,
				MemberExpression memberExpression => FromMemberExpression(memberExpression),
				_ => null
			};
		}

		private static object FromMemberExpression(MemberExpression memberExpression)
		{
			return memberExpression.Expression switch
			{
				null => GetValueFromExpression(
					memberExpression,
					value: null),

				ConstantExpression constantExpression => GetValueFromExpression(
					memberExpression,
					constantExpression.Value),

				_ => throw new InvalidExpressionException(
					$"{memberExpression.Expression.GetType()} expression is not supported. Create issue if that a mistake")
			};
		}

		private static object GetValueFromExpression(MemberExpression memberExpression, object value)
		{
			return memberExpression.Member switch
			{
				PropertyInfo propertyInfo => propertyInfo.GetMethod.Invoke(value, Array.Empty<object>()),
				FieldInfo fieldInfo => fieldInfo.GetValue(value),
				_ => throw new InvalidOperationException("Only fields and properties supported")
			};
		}

		private static ParameterDeclaration FromMethodCallExpression(
			MethodCallExpression expression,
			BindingSource bindingSource)
		{
			return new ParameterDeclaration(
				bindingSource,
				FromExpression(expression.Arguments.FirstOrDefault())?.ToString());
		}

		static RouteHelper()
		{
			ParameterDeclarationFactoryMap = new Dictionary<string, Func<MethodCallExpression, ParameterDeclaration>>
			{
				[nameof(From.Query)] = expression => FromMethodCallExpression(expression, BindingSource.Query),
				[nameof(From.Body)] = _ => new ParameterDeclaration(BindingSource.Body),
				[nameof(From.Route)] = expression => FromMethodCallExpression(expression, BindingSource.Path),
				[nameof(From.Header)] = expression => FromMethodCallExpression(expression, BindingSource.Header),
				[nameof(From.Services)] = _ => new ParameterDeclaration(BindingSource.Services),
				[nameof(From.Form)] = expression => FromMethodCallExpression(expression, BindingSource.Form),
				[nameof(From.Any)] = _ => new ParameterDeclaration(BindingSource.Custom),
				[nameof(From.ModelBinding)] = _ => new ParameterDeclaration(BindingSource.ModelBinding),
				[nameof(From.Special)] = _ => new ParameterDeclaration(BindingSource.Special),
				[nameof(From.FormFile)] = _ => new ParameterDeclaration(BindingSource.FormFile),
			};
		}
	}
}