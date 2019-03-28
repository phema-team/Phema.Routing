using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Phema.Routing
{
	internal sealed class ActionModelConvention : IActionModelConvention
	{
		private readonly IServiceProvider provider;
		private readonly RoutingOptions options;

		public ActionModelConvention(IServiceProvider provider, RoutingOptions options)
		{
			this.options = options;
			this.provider = provider;
		}

		public void Apply(ActionModel action)
		{
			if (!options.Actions.TryGetValue(action.ActionMethod, out var declaration))
			{
				return;
			}

			var model = new SelectorModel
			{
				AttributeRouteModel = declaration.Template == null
					? null
					: new AttributeRouteModel(
							new RouteAttribute(declaration.Template)
							{
								Name = declaration.Name
							})
			};

			foreach (var constraint in declaration.Constraints)
			{
				model.ActionConstraints.Add(constraint(provider));
			}

			foreach (var filter in declaration.Filters)
			{
				action.Filters.Add(filter(provider));
			}

			var conventions = action.Selectors.Where(s => s.AttributeRouteModel == null).ToList();

			foreach (var convention in conventions)
			{
				action.Selectors.Remove(convention);
			}

			action.Selectors.Insert(0, model);
		}
	}
}