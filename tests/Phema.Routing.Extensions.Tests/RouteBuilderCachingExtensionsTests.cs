using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Routing.Tests
{
	public class RouteBuilderCachingExtensionsTests
	{
		[Fact]
		public void EmptyCached()
		{
			var services = new ServiceCollection();
			
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.Cached()));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<PhemaConfigurationOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			var cache = Assert.IsType<ResponseCacheAttribute>(filter);
			
			Assert.Equal(30, cache.Duration);
			Assert.False(cache.NoStore);
			Assert.Null(cache.VaryByHeader);
			Assert.Null(cache.CacheProfileName);
			Assert.Null(cache.VaryByQueryKeys);
		}
		
		[Fact]
		public void CachedWithParameters()
		{
			var services = new ServiceCollection();
			
			services.AddMvcCore()
				.AddPhemaRouting(routing =>
					routing.AddController<TestController>("test", controller =>
						controller.AddRoute("works", c => c.TestMethod(From.Query<string>()))
							.Cached(
								duration: 12,
								noStore: true,
								profile: "profile",
								header: "header",
								query: new[]{ "query" })));

			var provider = services.BuildServiceProvider();

			var options = provider.GetRequiredService<IOptions<PhemaConfigurationOptions>>().Value;
			
			var (_, actionMetadata) = Assert.Single(options.Actions);
			var filter = Assert.Single(actionMetadata.Filters)(provider);

			var cache = Assert.IsType<ResponseCacheAttribute>(filter);
			
			Assert.Equal(12, cache.Duration);
			Assert.True(cache.NoStore);
			Assert.Equal("header", cache.VaryByHeader);
			Assert.Equal("profile", cache.CacheProfileName);
			Assert.Equal("query", Assert.Single(cache.VaryByQueryKeys));
		}
	}
}