using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Phema.Routing
{
	public interface IRouteBuilder
	{
		IRouteBuilder AddFilter(Func<IServiceProvider, IFilterMetadata> selector);

		IRouteBuilder AddConstraint(Func<IServiceProvider, IActionConstraintMetadata> selector);

		IRouteBuilder WithName(string name);
	}
}