using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Phema.Routing
{
	// TODO: Remove when Microsoft.AspNetCore.Mvc.ActionConstraints.HttpMethodActionConstraint make public again
	internal sealed class HttpMethodActionConstraint : IActionConstraint
	{
		public HttpMethodActionConstraint(IEnumerable<string> httpMethods)
		{
			HttpMethods = httpMethods;
		}

		public IEnumerable<string> HttpMethods { get; }

		public int Order => 100;

		public bool Accept(ActionConstraintContext context)
		{
			if (!HttpMethods.Any())
			{
				return true;
			}

			var method = context.RouteContext.HttpContext.Request.Method;

			return HttpMethods.Any(httpMethod => string.Equals(httpMethod, method, StringComparison.OrdinalIgnoreCase));
		}
	}
}