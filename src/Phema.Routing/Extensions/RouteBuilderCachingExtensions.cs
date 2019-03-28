using Microsoft.AspNetCore.Mvc;

namespace Phema.Routing
{
	public static class RouteBuilderCachingExtensions
	{
		public static IRouteBuilder Cached(
			this IRouteBuilder builder, 
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

			return builder.AddFilter(filter);
		}
	}
}