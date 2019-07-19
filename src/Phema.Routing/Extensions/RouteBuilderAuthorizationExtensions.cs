using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Phema.Routing
{
	public static class RouteBuilderAuthorizationExtensions
	{
		public static TBuilder Authorize<TBuilder>(
			this TBuilder routeBuilder,
			params string[] policies)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			var attributes = policies.Any()
				? policies.Select(policy => new AuthorizeAttribute(policy))
				: new[] {new AuthorizeAttribute()};

			return routeBuilder.AddFilter(new AuthorizeFilter(attributes));
		}

		public static TBuilder AllowAnonymous<TBuilder>(
			this TBuilder routeBuilder)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return routeBuilder.AddFilter(new AllowAnonymousFilter());
		}
	}
}