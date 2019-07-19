
namespace Phema.Routing
{
	public static class RouteBuilderApiExtensions
	{
		public static IControllerActionRouteBuilder Produces<TProduces>(
			this IControllerActionRouteBuilder routeBuilder,
			int statusCode,
			params string[] contentTypes)
		{
			return routeBuilder.AddFilter(new ApiResponseMetadataProvider(typeof(TProduces), statusCode, contentTypes));
		}

		public static IControllerActionRouteBuilder Consumes(
			this IControllerActionRouteBuilder routeBuilder,
			params string[] contentTypes)
		{
			return routeBuilder.AddFilter(new ApiRequestMetadataProvider(contentTypes));
		}
	}
}