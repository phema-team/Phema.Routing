using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Phema.Routing
{
	public static class RouteBuilderAuthorizationExtensions
	{
		public static IRouteBuilder Authorize(this IRouteBuilder builder, params string[] policies)
		{
			var attributes = policies.Any()
				? policies.Select(policy => new AuthorizeAttribute(policy))
				: new[] { new AuthorizeAttribute() };
			
			return builder.AddFilter(new AuthorizeFilter(attributes));
		}

		public static IRouteBuilder AllowAnonymous(this IRouteBuilder builder)
		{
			return builder.AddFilter(new AllowAnonymousFilter());
		}
	}
}