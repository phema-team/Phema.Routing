using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Phema.Routing
{
	internal sealed class RouteDeclaration
	{
		public RouteDeclaration(string template)
		{
			Template = template;
			Filters = new List<Func<IServiceProvider, IFilterMetadata>>();
			Constraints = new List<Func<IServiceProvider, IActionConstraintMetadata>>();
		}

		public string Template { get; }
		public IList<Func<IServiceProvider, IFilterMetadata>> Filters { get; }
		public IList<Func<IServiceProvider, IActionConstraintMetadata>> Constraints { get; }
		public string Name { get; set; }
	}
}