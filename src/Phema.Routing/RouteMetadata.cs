using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Phema.Routing
{
	internal class RouteMetadata
	{
		public RouteMetadata(string template)
		{
			Template = template;
			Constraints = new List<Func<IServiceProvider, IActionConstraintMetadata>>();
			Filters = new List<Func<IServiceProvider, IFilterMetadata>>();
		}

		public string Template { get; }
		public IList<Func<IServiceProvider, IActionConstraintMetadata>> Constraints { get; }
		public IList<Func<IServiceProvider, IFilterMetadata>> Filters { get; }
		public string Name { get; set; }
	}
}