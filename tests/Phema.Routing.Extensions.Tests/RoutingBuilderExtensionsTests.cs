using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Routing.Tests
{
	public class RoutingBuilderExtensionsTests
	{
		private readonly IServiceCollection services;

		public RoutingBuilderExtensionsTests()
		{
			services = new ServiceCollection();
		}

		[Fact]
		public void AddRouteExtensionAddsEmptyRouteTemplate()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>(controller => {}));

			var provider = services.BuildServiceProvider();

			var controllertype = typeof(TestController);
			var options = provider.GetRequiredService<IOptions<RoutingConfigurationOptions>>().Value;

			var (controllerTypeInfo, controllerMetadata) = Assert.Single(options.Controllers);
			Assert.Equal(controllertype.GetTypeInfo(), controllerTypeInfo);
			Assert.Equal(string.Empty, controllerMetadata.Template);
		}
	}
}