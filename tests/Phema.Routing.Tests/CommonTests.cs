using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Routing.Tests
{
	public class CommonTests
	{
		private readonly IServiceCollection services;

		public CommonTests()
		{
			services = new ServiceCollection();
		}

		[Fact]
		public void AddRoutingAddsPostConfiguration()
		{
			services.AddMvcCore()
				.AddRouting(routing => {});

			Assert.Single(services.Where(x => x.ImplementationType == typeof(RoutingPostConfigureOptions)));
		}

		[Fact]
		public void AddRoutingControllerActionAndParameter()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))));

			var provider = services.BuildServiceProvider();

			var controllertype = typeof(TestController);
			var options = provider.GetRequiredService<IOptions<RoutingOptions>>().Value;
			
			var (controllerTypeInfo, controllerMetadata) = Assert.Single(options.Controllers);
			Assert.Equal(controllertype.GetTypeInfo(), controllerTypeInfo);
			Assert.Equal("test", controllerMetadata.Template);
			
			var (actionMemberInfo, actionMetadata) = Assert.Single(options.Actions);
			Assert.Equal(controllertype.GetMember(nameof(TestController.TestMethod)).Single(), actionMemberInfo);
			Assert.Equal("works", actionMetadata.Template);
			
 			var (parameterInfo, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(typeof(string), parameterInfo.ParameterType);
			Assert.Equal(nameof(From.Query), parameterMetadata.BindingSource.DisplayName);
		}
		
		[Fact]
		public void AddActionName()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.Named("name")));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<RoutingOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			Assert.Equal("name", actionMetadata.ActionName);
		}
		
		[Fact]
		public void AddControllerName()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller => {})
						.Named("name"));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<RoutingOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Controllers);
			Assert.Equal("name", actionMetadata.ActionName);
		}
		
		[Fact]
		public void AddActionConstraint()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.AddConstraint(sp => new HttpMethodActionConstraint(new[] { "method" }))));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<RoutingOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			var constraint = Assert.Single(actionMetadata.ActionConstraints)(provider);

			var methodActionConstraint = Assert.IsType<HttpMethodActionConstraint>(constraint);
			
			Assert.Equal("method", Assert.Single(methodActionConstraint.HttpMethods));
		}
		
		[Fact]
		public void AddControllerConstraint()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller => {})
						.AddConstraint(sp => new HttpMethodActionConstraint(new[] { "method" })));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<RoutingOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Controllers);
			var constraint = Assert.Single(actionMetadata.ActionConstraints)(provider);

			var methodActionConstraint = Assert.IsType<HttpMethodActionConstraint>(constraint);
			
			Assert.Equal("method", Assert.Single(methodActionConstraint.HttpMethods));
		}
		
		[Fact]
		public void AddActionFilter()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.AddFilter(sp => new AllowAnonymousFilter())));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<RoutingOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			Assert.IsType<AllowAnonymousFilter>(filter);
		}
		
		[Fact]
		public void AddControllerFilter()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller => {})
						.AddFilter(sp => new AllowAnonymousFilter()));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<RoutingOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Controllers);
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			Assert.IsType<AllowAnonymousFilter>(filter);
		}
	}
}