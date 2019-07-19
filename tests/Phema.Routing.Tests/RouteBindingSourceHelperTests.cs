using System;
using System.Linq.Expressions;
using Xunit;

namespace Phema.Routing.Tests
{
	public class RouteBindingSourceHelperTests
	{
		[Fact]
		public void FromQuerySetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Query<string>("somename"));

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromQuerySetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Query<string>());

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromHeaderSetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Header<string>("somename"));

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromHeaderSetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Header<string>());

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromFormSetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Form<string>("somename"));

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromFormSetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Form<string>());

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}
		
		[Fact]
		public void FromRouteSetsParameterName()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Route<string>("somename"));

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Equal("somename", routeParameter.declaration.ModelName);
		}

		[Fact]
		public void FromRouteSetsParameterNameWithoutOverride()
		{
			Expression<Func<TestController, string>> expression = c => c.TestMethod(From.Route<string>());

			var methodCall = RouteBindingSourceHelper.GetInnerMethodCallExpression(expression);

			var routeParameters = RouteBindingSourceHelper.GetRouteParameters(methodCall);

			var routeParameter = Assert.Single(routeParameters);

			Assert.Null(routeParameter.declaration.ModelName);
		}
	}
}