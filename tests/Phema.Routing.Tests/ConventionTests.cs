using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Routing.Tests
{
	public class ConventionTests
	{
		private readonly IServiceCollection services;

		public ConventionTests()
		{
			services = new ServiceCollection();
		}

		[Fact]
		public void ControllerConvention()
		{
			var provider = services.BuildServiceProvider();

			var controllerTypeInfo = typeof(TestController).GetTypeInfo();
			
			var convention = new ControllerModelConvention(provider, new RoutingOptions
			{
				Controllers =
				{
					[controllerTypeInfo] = new RouteDeclaration("test")
					{
						Name = "name",
						Filters = { sp => new RequireHttpsAttribute()},
						Constraints = { sp => new HttpMethodActionConstraint(new [] { "method" })}
					}
				}
			});

			var model = new ControllerModel(controllerTypeInfo, Array.Empty<object>());
			
			convention.Apply(model);

			Assert.Empty(model.Actions);
			Assert.Empty(model.Attributes);
			Assert.Empty(model.ControllerProperties);
			Assert.Equal(model.ControllerType, controllerTypeInfo);
			Assert.Empty(model.Properties);
			Assert.Empty(model.RouteValues);
			Assert.IsType<RequireHttpsAttribute>(Assert.Single(model.Filters));
			var selector = Assert.Single(model.Selectors);
			
			var methodConstraint = Assert.IsType<HttpMethodActionConstraint>(Assert.Single(selector.ActionConstraints));
			
			Assert.Equal("method", Assert.Single(methodConstraint.HttpMethods));
			
			Assert.Equal("name", selector.AttributeRouteModel.Name);
			Assert.Equal("test", selector.AttributeRouteModel.Template);
			Assert.IsType<RouteAttribute>(selector.AttributeRouteModel.Attribute);
		}
		
		[Fact]
		public void ActionConvention()
		{
			var provider = services.BuildServiceProvider();

			var actionMethodInfo = typeof(TestController).GetMethod(nameof(TestController.TestMethod));
			
			var convention = new ActionModelConvention(provider, new RoutingOptions
			{
				Actions =
				{
					[actionMethodInfo] = new RouteDeclaration("test")
					{
						Name = "name",
						Filters = { sp => new RequireHttpsAttribute()},
						Constraints = { sp => new HttpMethodActionConstraint(new [] { "method" })}
					}
				}
			});

			var model = new ActionModel(actionMethodInfo, Array.Empty<object>());
			
			convention.Apply(model);

			Assert.Empty(model.Parameters);
			Assert.Empty(model.Attributes);
			Assert.Equal(model.ActionMethod, actionMethodInfo);
			Assert.Empty(model.Properties);
			Assert.Empty(model.RouteValues);
			Assert.IsType<RequireHttpsAttribute>(Assert.Single(model.Filters));
			var selector = Assert.Single(model.Selectors);
			
			var methodConstraint = Assert.IsType<HttpMethodActionConstraint>(Assert.Single(selector.ActionConstraints));
			
			Assert.Equal("method", Assert.Single(methodConstraint.HttpMethods));
			
			Assert.Equal("name", selector.AttributeRouteModel.Name);
			Assert.Equal("test", selector.AttributeRouteModel.Template);
			Assert.IsType<RouteAttribute>(selector.AttributeRouteModel.Attribute);
		}
		
		[Fact]
		public void ParameterConvention()
		{
			var parameterInfo = typeof(TestController).GetMethod(nameof(TestController.TestMethod)).GetParameters().Single();
			
			var convention = new ParameterModelConvention(new RoutingOptions
			{
				Parameters =
				{
					[parameterInfo] = new ParameterDeclaration(BindingSource.Query, "name")
				}
			});

			var model = new ParameterModel(parameterInfo, Array.Empty<object>());
			
			convention.Apply(model);

			Assert.Empty(model.Attributes);
			Assert.Equal(BindingSource.Query, model.BindingInfo.BindingSource);
			Assert.Equal("name", model.BindingInfo.BinderModelName);
			Assert.Equal(typeof(string), model.ParameterInfo.ParameterType);
			Assert.Empty(model.Properties);
		}
	}
}