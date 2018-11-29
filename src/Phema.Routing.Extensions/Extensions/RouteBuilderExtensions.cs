using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

[assembly: InternalsVisibleTo("Phema.Routing.Extensions.Tests")]

namespace Phema.Routing
{
	public static class RouteBuilderExtensions
	{
		internal const string HttpHeadMethod = "HEAD";
		internal const string HttpGetMethod = "GET";
		internal const string HttpPostMethod = "POST";
		internal const string HttpPutMethod = "PUT";
		internal const string HttpDeleteMethod = "DELETE";
		
		public static IRouteBuilder AddConstraint(this IRouteBuilder builder, IActionConstraintMetadata constraint)
		{
			return builder.AddConstraint(sp => constraint);
		}
		
		public static IRouteBuilder AddFilter(this IRouteBuilder builder, IFilterMetadata filter)
		{
			return builder.AddFilter(sp => filter);
		}
		
		public static IRouteBuilder HttpHead(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(new[] { HttpHeadMethod }));
		}
		
		public static IRouteBuilder HttpGet(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(new[] { HttpGetMethod }));
		}
		
		public static IRouteBuilder HttpGetOrHead(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(new[] { HttpHeadMethod, HttpGetMethod }));
		}

		public static IRouteBuilder HttpPost(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(new[] { HttpPostMethod }));
		}

		public static IRouteBuilder HttpPut(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(new[] { HttpPutMethod }));
		}

		public static IRouteBuilder HttpDelete(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(new[] { HttpDeleteMethod }));
		}
	}
}