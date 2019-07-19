using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Phema.Routing
{
	public static class RouteBuilderExtensions
	{
		public static TBuilder AddConstraint<TBuilder>(
			this TBuilder builder,
			Func<IServiceProvider, IActionConstraintMetadata> selector)
			where TBuilder : IRouteBuilder<TBuilder>
		{
			builder.Declaration.Constraints.Add(selector);

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
			builder.Declaration.Name = name;

			return builder;
		}
	}
}