using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Phema.Routing
{
	public static class RouteBuilderExtensions
	{
		public static TBuilder AddConstraint<TBuilder>(
			this TBuilder builder,
			Func<IServiceProvider, IActionConstraintMetadata> selector)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			builder.Declaration.ActionConstraints.Add(selector);

			return builder;
		}

		public static TBuilder AddConstraint<TBuilder>(
			this TBuilder builder,
			IActionConstraintMetadata constraint)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return builder.AddConstraint(sp => constraint);
		}

		public static TBuilder AddFilter<TBuilder>(
			this TBuilder builder,
			Func<IServiceProvider, IFilterMetadata> selector)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			builder.Declaration.Filters.Add(selector);

			return builder;
		}

		public static TBuilder AddFilter<TBuilder>(
			this TBuilder builder,
			IFilterMetadata filter)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return builder.AddFilter(sp => filter);
		}

		public static TBuilder Named<TBuilder>(this TBuilder builder, string name)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			builder.Declaration.ActionName = name;

			return builder;
		}

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

		public static TBuilder ValidateAntiForgeryToken<TBuilder>(
			this TBuilder routeBuilder)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return routeBuilder.AddFilter(new ValidateAntiForgeryTokenAttribute());
		}

		public static TBuilder IgnoreAntiforgeryToken<TBuilder>(
			this TBuilder routeBuilder)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return routeBuilder.AddFilter(new IgnoreAntiforgeryTokenAttribute());
		}

		public static TBuilder RequireHttps<TBuilder>(
			this TBuilder routeBuilder)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return routeBuilder.AddFilter(new RequireHttpsAttribute());
		}

		public static TBuilder RequestSizeLimit<TBuilder>(
			this TBuilder routeBuilder,
			long limit)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return routeBuilder.AddFilter(new RequestSizeLimitAttribute(limit));
		}

		public static TBuilder DisableRequestSizeLimit<TBuilder>(
			this TBuilder routeBuilder)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			return routeBuilder.AddFilter(new DisableRequestSizeLimitAttribute());
		}
	}
}