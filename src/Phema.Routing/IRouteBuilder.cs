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

	internal sealed class RouteBuilder : IRouteBuilder
	{
		private readonly RouteMetadata metadata;

		public RouteBuilder(RouteMetadata metadata)
		{
			this.metadata = metadata;
		}

		public IRouteBuilder AddFilter(Func<IServiceProvider, IFilterMetadata> selector)
		{
			metadata.Filters.Add(selector);
			return this;
		}
		
		public IRouteBuilder AddConstraint(Func<IServiceProvider, IActionConstraintMetadata> selector)
		{
			metadata.Constraints.Add(selector);
			return this;
		}

		public IRouteBuilder WithName(string name)
		{
			metadata.Name = name;
			return this;
		}
	}
}