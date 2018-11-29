using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Phema.Routing
{
	internal sealed class ActionModelConvention : IActionModelConvention
	{
		private readonly RoutingOptions options;
		private readonly IServiceProvider provider;

		public ActionModelConvention(IServiceProvider provider, RoutingOptions options)
		{
			this.options = options;
			this.provider = provider;
		}

		public void Apply(ActionModel action)
		{
			var metadata = options.Actions[action.ActionMethod];

			var model = new SelectorModel
			{
				AttributeRouteModel = metadata.Template == null
					? null
					: new AttributeRouteModel(
						new RouteAttribute(metadata.Template)
						{
							Name = metadata.Name
						})
			};

			foreach (var constraint in metadata.Constraints)
			{
				model.ActionConstraints.Add(constraint(provider));
			}

			foreach (var filter in metadata.Filters)
			{
				action.Filters.Add(filter(provider));
			}
			
			action.Selectors.Clear();
			action.Selectors.Add(model);
		}
	}
}