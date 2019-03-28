using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Phema.Routing
{
	internal sealed class HttpMethodActionConstraint : IActionConstraint
	{
		public HttpMethodActionConstraint(string httpMethod)
		{
			HttpMethods = new[] { httpMethod };
		}

		public HttpMethodActionConstraint(params string[] httpMethods)
		{
			HttpMethods = httpMethods;
		}

		public IEnumerable<string> HttpMethods { get; }

		public bool Accept(ActionConstraintContext context)
		{
			var method = context.RouteContext.HttpContext.Request.Method;

			return HttpMethods.Any(httpMethod => httpMethod == method);
		}

		public int Order => 100;
	}
}