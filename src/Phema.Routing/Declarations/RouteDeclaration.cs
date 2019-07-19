using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Phema.Routing
{
	public interface IRouteDeclaration
	{
		string Template { get; }
		IList<Func<IServiceProvider, IFilterMetadata>> Filters { get; }
		IList<Func<IServiceProvider, IActionConstraintMetadata>> Constraints { get; }
		string Name { get; set; }
	}

	internal sealed class RouteDeclaration : IRouteDeclaration
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