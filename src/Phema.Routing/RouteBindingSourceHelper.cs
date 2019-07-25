using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phema.Routing
{
	internal static class RouteBindingSourceHelper
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

			throw new InvalidOperationException($"Only {nameof(MethodCallExpression)}'s allowed");
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

		static RouteBindingSourceHelper()
		{
			ParameterDeclarationFactoryMap = new Dictionary<string, Func<MethodCallExpression, ParameterDeclaration>>
			{
				[nameof(From.Query)] = expression => GetParameterDeclarationFromExpression(expression, BindingSource.Query),
				[nameof(From.Body)] = _ => new ParameterDeclaration(BindingSource.Body),
				[nameof(From.Route)] = expression => GetParameterDeclarationFromExpression(expression, BindingSource.Path),
				[nameof(From.Header)] = expression => GetParameterDeclarationFromExpression(expression, BindingSource.Header),
				[nameof(From.Services)] = _ => new ParameterDeclaration(BindingSource.Services),
				[nameof(From.Form)] = expression => GetParameterDeclarationFromExpression(expression, BindingSource.Form),
				[nameof(From.Any)] = _ => new ParameterDeclaration(BindingSource.Custom),
				[nameof(From.ModelBinding)] = _ => new ParameterDeclaration(BindingSource.ModelBinding),
				[nameof(From.Special)] = _ => new ParameterDeclaration(BindingSource.Special),
				[nameof(From.FormFile)] = _ => new ParameterDeclaration(BindingSource.FormFile),
			};
		}

		private static ParameterDeclaration GetParameterDeclarationFromExpression(
			MethodCallExpression expression,
			BindingSource bindingSource)
		{
			var nameExpression = expression.Arguments.FirstOrDefault();

			var modelName = nameExpression switch
			{
				null => null,
				ConstantExpression constant => (string) constant.Value,
				MemberExpression memberExpression => GetFromMemberExpression(memberExpression),
				_ => throw new InvalidOperationException($"Invalid expression type: {nameExpression.GetType()}")
			};

			return new ParameterDeclaration(bindingSource, modelName);
		}

		private static string GetFromMemberExpression(MemberExpression memberExpression)
		{
			return memberExpression.Expression switch
			{
				null => GetByMemberType(memberExpression, memberExpression.Member.DeclaringType, null),
				ConstantExpression constantExpression =>
				GetByMemberType(memberExpression, memberExpression.Member.DeclaringType, constantExpression.Value),
				_ => throw new InvalidOperationException(
					$"Only {nameof(ConstantExpression)} allowed. Literal constants prefered")
			};
		}

		private static string GetByMemberType(MemberExpression memberExpression, Type type, object value)
		{
			return memberExpression.Member.MemberType switch
			{
				MemberTypes.Field => type
					.GetField(memberExpression.Member.Name,
						BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.GetValue(value)
					.ToString(),
				MemberTypes.Property => type
					.GetProperty(memberExpression.Member.Name,
						BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.GetValue(value)
					.ToString(),
				_ => throw new InvalidOperationException("Only fields and properties supported")
			};
		}
	}
}