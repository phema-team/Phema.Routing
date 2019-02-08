using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Routing.Tests
{
	public class RouteBuilderAuthorizationExtensionsTests
	{
		[Fact]
		public void EmptyAuthorize()
		{
			var services = new ServiceCollection();
			
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.Authorize()));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			var authorize = Assert.IsType<AuthorizeFilter>(filter);

			Assert.Null(authorize.Policy);
			Assert.Null(authorize.PolicyProvider);
			
			var attribute = Assert.IsType<AuthorizeAttribute>(Assert.Single(authorize.AuthorizeData));
			
			Assert.Null(attribute.Roles);
			Assert.Null(attribute.Policy);
			Assert.Null(attribute.AuthenticationSchemes);
		}
		
		[Fact]
		public void AuthorizeWithPolicy()
		{
			var services = new ServiceCollection();
			
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.Authorize("policy")));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			var authorize = Assert.IsType<AuthorizeFilter>(filter);

			Assert.Null(authorize.Policy);
			Assert.Null(authorize.PolicyProvider);
			
			var attribute = Assert.IsType<AuthorizeAttribute>(Assert.Single(authorize.AuthorizeData));
			
			Assert.Null(attribute.Roles);
			Assert.Equal("policy", attribute.Policy);
			Assert.Null(attribute.AuthenticationSchemes);
		}
		
		
		[Fact]
		public void AuthorizeWithMultiplePolicies()
		{
			var services = new ServiceCollection();
			
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.Authorize("policy1", "policy2")));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			var authorize = Assert.IsType<AuthorizeFilter>(filter);

			Assert.Null(authorize.Policy);
			Assert.Null(authorize.PolicyProvider);
			
			Assert.Collection(authorize.AuthorizeData,
				data =>
				{
					var attribute = Assert.IsType<AuthorizeAttribute>(data);
					
					Assert.Null(attribute.Roles);
					Assert.Equal("policy1", attribute.Policy);
					Assert.Null(attribute.AuthenticationSchemes);
				},
				data =>
				{
					var attribute = Assert.IsType<AuthorizeAttribute>(data);
					
					Assert.Null(attribute.Roles);
					Assert.Equal("policy2", attribute.Policy);
					Assert.Null(attribute.AuthenticationSchemes);
				});
		}

		[Fact]
		public void AllowAnonymous()
		{
			var services = new ServiceCollection();
			
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.AllowAnonymous()));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			Assert.IsType<AllowAnonymousFilter>(filter);
		}
	}
}