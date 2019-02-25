
namespace Phema.Routing
{
	public static class RouteBuilderApiExtensions
	{
		public static IRouteBuilder Produces<TProduces>(
			this IRouteBuilder builder,
			int statusCode,
			params string[] contentTypes)
		{
			return builder.AddFilter(new PhemaApiResponseMetadataProvider(typeof(TProduces), statusCode, contentTypes));
		}

		public static IRouteBuilder Consumes(this IRouteBuilder builder, params string[] contentTypes)
		{
			return builder.AddFilter(new PhemaApiRequestMetadataProvider(contentTypes));
		}
	}
}