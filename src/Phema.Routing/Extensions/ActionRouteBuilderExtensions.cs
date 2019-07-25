using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Phema.Routing
{
	public static class ControllerActionRouteBuilderExtensions
	{
		public static IActionRouteBuilder HttpMethod(
			this IActionRouteBuilder builder,
			params string[] httpMethods)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(httpMethods));
		}

		public static IActionRouteBuilder HttpHead(
			this IActionRouteBuilder builder)
		{
			return builder.HttpMethod(HttpMethods.Head);
		}

		public static IActionRouteBuilder HttpGet(
			this IActionRouteBuilder builder)
		{
			return builder.HttpMethod(HttpMethods.Get);
		}

		public static IActionRouteBuilder HttpPost(
			this IActionRouteBuilder builder)
		{
			return builder.HttpMethod(HttpMethods.Post);
		}

		public static IActionRouteBuilder HttpPut(
			this IActionRouteBuilder builder)
		{
			return builder.HttpMethod(HttpMethods.Put);
		}

		public static IActionRouteBuilder HttpPatch(
			this IActionRouteBuilder builder)
		{
			return builder.HttpMethod(HttpMethods.Patch);
		}

		public static IActionRouteBuilder HttpDelete(
			this IActionRouteBuilder builder)
		{
			return builder.HttpMethod(HttpMethods.Delete);
		}

		public static IActionRouteBuilder HttpOptions(
			this IActionRouteBuilder builder)
		{
			return builder.HttpMethod(HttpMethods.Options);
		}

		public static IActionRouteBuilder Cached(
			this IActionRouteBuilder routeBuilder,
			int duration = 30,
			string header = null,
			string profile = null,
			bool noStore = false,
			string[] query = null)
		{
			var filter = new ResponseCacheAttribute
			{
				Duration = duration,
				NoStore = noStore,
				VaryByHeader = header,
				CacheProfileName = profile,
				VaryByQueryKeys = query
			};

			return routeBuilder.AddFilter(filter);
		}

		public static IActionRouteBuilder Produces<TProduces>(
			this IActionRouteBuilder routeBuilder,
			int statusCode,
			params string[] contentTypes)
		{
			return routeBuilder.AddFilter(new ApiResponseMetadataProvider<TProduces>(statusCode, contentTypes));
		}

		public static IActionRouteBuilder Consumes(
			this IActionRouteBuilder routeBuilder,
			params string[] contentTypes)
		{
			return routeBuilder.AddFilter(new ApiRequestMetadataProvider(contentTypes));
		}
	}
}