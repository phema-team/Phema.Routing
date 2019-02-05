using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Routing.Tests
{
	public class FromTests
	{
		private readonly IServiceCollection services;

		public FromTests()
		{
			services = new ServiceCollection();
		}
		
		[Fact]
		public void FromTypeEquality()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;

			var (parameterInfo, _) = Assert.Single(options.Parameters);
			Assert.Equal(typeof(string), parameterInfo.ParameterType);
		}

		[Fact]
		public void FromQuery()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Query, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromAny()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Any<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Custom, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromBody()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Body<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Body, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromForm()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Form<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Form, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromRoute()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Route<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Path, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromHeader()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Header<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Header, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromServices()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Services<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Services, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromSpecial()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Special<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.Special, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromModelBinding()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.ModelBinding<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.ModelBinding, parameterMetadata.BindingSource);
		}
		
		[Fact]
		public void FromFormFile()
		{
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.FormFile<string>()))));

			var provider = services.BuildServiceProvider();
			var options = provider.GetRequiredService<IOptions<PhemaRoutingConfigurationOptions>>().Value;
			
			var (_, parameterMetadata) = Assert.Single(options.Parameters);
			Assert.Equal(BindingSource.FormFile, parameterMetadata.BindingSource);
		}
	}
}