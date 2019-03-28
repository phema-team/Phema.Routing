using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ActionConstraints;


namespace Phema.Routing
{
	public static class RouteBuilderExtensions
	{
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
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Head));
		}
		
		public static IRouteBuilder HttpGet(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Get));
		}
		
		public static IRouteBuilder HttpGetOrHead(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Head, HttpMethods.Get));
		}

		public static IRouteBuilder HttpPost(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Post));
		}

		public static IRouteBuilder HttpPut(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Put));
		}

		public static IRouteBuilder HttpDelete(this IRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Delete));
		}
	}
}