using Microsoft.AspNetCore.Mvc.Authorization;

namespace Phema.Routing
{
	public static class RouteBuilderAuthorizationExtensions
	{
		public static IRouteBuilder Authorize(this IRouteBuilder builder, string policy = null)
		{
			return builder.AddFilter(new AuthorizeFilter(policy));
		}

		public static IRouteBuilder AllowAnonymous(this IRouteBuilder builder)
		{
			return builder.AddFilter(new AllowAnonymousFilter());
		}
	}
}