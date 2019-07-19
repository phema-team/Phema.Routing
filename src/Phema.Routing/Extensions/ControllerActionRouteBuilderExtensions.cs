using Microsoft.AspNetCore.Http;

namespace Phema.Routing
{
	public static class ControllerActionRouteBuilderExtensions
	{
		
		public static IControllerActionRouteBuilder HttpHead(
			this IControllerActionRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Head));
		}

		public static IControllerActionRouteBuilder HttpGet(
			this IControllerActionRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Get));
		}

		public static IControllerActionRouteBuilder HttpGetOrHead(
			this IControllerActionRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Head, HttpMethods.Get));
		}

		public static IControllerActionRouteBuilder HttpPost(
			this IControllerActionRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Post));
		}

		public static IControllerActionRouteBuilder HttpPut(
			this IControllerActionRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Put));
		}

		public static IControllerActionRouteBuilder HttpDelete(
			this IControllerActionRouteBuilder builder)
		{
			return builder.AddConstraint(new HttpMethodActionConstraint(HttpMethods.Delete));
		}
	}
}