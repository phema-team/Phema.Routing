using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Routing.Tests
{
	public class ControllerBuilderExtensionsTests
	{
		private readonly IServiceCollection services;

		public ControllerBuilderExtensionsTests()
		{
			services = new ServiceCollection();
		}

		[Fact]
		public void AddRouteExtensionAddsEmptyRouteTemplate()
		{
			services.AddMvcCore()
				.AddRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute(c => c.TestMethod(From.Query<string>()))));

			var provider = services.BuildServiceProvider();

			var controllertype = typeof(TestController);
			var options = provider.GetRequiredService<IOptions<RoutingConfigurationOptions>>().Value;

			var (actionMemberInfo, actionMetadata) = Assert.Single(options.Actions);
			Assert.Equal(controllertype.GetMember(nameof(TestController.TestMethod)).Single(), actionMemberInfo);
			Assert.Equal(string.Empty, actionMetadata.Template);
		}
	}
}