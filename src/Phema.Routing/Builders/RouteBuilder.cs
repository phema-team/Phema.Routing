using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Phema.Routing
{
	public interface IRouteBuilder
	{
		IRouteBuilder AddFilter(Func<IServiceProvider, IFilterMetadata> selector);

		IRouteBuilder AddConstraint(Func<IServiceProvider, IActionConstraintMetadata> selector);

		IRouteBuilder WithName(string name);
	}
	
	internal sealed class RouteBuilder : IRouteBuilder
	{
		private readonly RouteDeclaration declaration;

		public RouteBuilder(RouteDeclaration declaration)
		{
			this.declaration = declaration;
		}

		public IRouteBuilder AddFilter(Func<IServiceProvider, IFilterMetadata> selector)
		{
			declaration.Filters.Add(selector);
			return this;
		}
		
		public IRouteBuilder AddConstraint(Func<IServiceProvider, IActionConstraintMetadata> selector)
		{
			declaration.Constraints.Add(selector);
			return this;
		}

		public IRouteBuilder WithName(string name)
		{
			declaration.Name = name;
			return this;
		}
	}
}