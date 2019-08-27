using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit;

namespace Phema.Routing.Tests
{
	public class RouteHelperTests
	{
		[Fact]
		public void FromQuerySetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Query<string>("somename"));

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromQuerySetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Query<string>());

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromHeaderSetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Header<string>("somename"));

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromHeaderSetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Header<string>());

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromFormSetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Form<string>("somename"));

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromFormSetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Form<string>());

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromRouteSetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Route<string>("somename"));

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromRouteSetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Route<string>());

			var methodCall = RouteHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}

		[Fact]
		public void GetActionArguments_Empty()
		{
			var expression = RouteHelper.GetInnerMethodCallExpression<TestController, string>(
				c => c.TestMethod(From.Route<string>()));

			var parameters = new[] {new ParameterDescriptor {BindingInfo = new BindingInfo()}};
			var arguments = RouteHelper.GetActionArguments(parameters, expression);

			Assert.Empty(arguments);
		}

		[Fact]
		public void GetActionArguments_Constant()
		{
			var expression = RouteHelper.GetInnerMethodCallExpression<TestController, string>(
				c => c.TestMethod("value"));

			var parameters = new[] {new ParameterDescriptor {BindingInfo = new BindingInfo(), Name = "parameter"}};
			var arguments = RouteHelper.GetActionArguments(parameters, expression);

			var (key, value) = Assert.Single(arguments);

			Assert.Equal("parameter", key);
			Assert.Equal("value", value);
		}

		[Fact]
		public void GetActionArguments_Local()
		{
			var localValueParameter = "value";

			var expression = RouteHelper.GetInnerMethodCallExpression<TestController, string>(
				c => c.TestMethod(localValueParameter));

			var parameters = new[] {new ParameterDescriptor {BindingInfo = new BindingInfo(), Name = "parameter"}};

			var arguments = RouteHelper.GetActionArguments(parameters, expression);

			var (key, value) = Assert.Single(arguments);

			Assert.Equal("parameter", key);
			Assert.Equal("value", value);
		}
	}
}