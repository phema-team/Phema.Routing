using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Phema.Routing
{
	internal sealed class ActionModelConvention : IActionModelConvention
	{
		private readonly IServiceProvider provider;
		private readonly PhemaConfigurationOptions options;

		public ActionModelConvention(IServiceProvider provider, PhemaConfigurationOptions options)
		{
			this.options = options;
			this.provider = provider;
		}

		public void Apply(ActionModel action)
		{
			if (!options.Actions.TryGetValue(action.ActionMethod, out var metadata))
				return;

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
				model.ActionConstraints.Add(constraint(provider));

			foreach (var filter in metadata.Filters)
				action.Filters.Add(filter(provider));


			var conventions = action.Selectors.Where(s => s.AttributeRouteModel == null).ToList();

			foreach (var convention in conventions)
				action.Selectors.Remove(convention);
				
			action.Selectors.Insert(0, model);
		}
	}
}