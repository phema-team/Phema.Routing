using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Phema.Routing
{
	internal sealed class ControllerModelConvention : IControllerModelConvention
	{
		private readonly RoutingConfigurationOptions options;
		private readonly IServiceProvider provider;

		public ControllerModelConvention(IServiceProvider provider, RoutingConfigurationOptions options)
		{
			this.options = options;
			this.provider = provider;
		}
		
		public void Apply(ControllerModel controller)
		{
			var metadata = options.Controllers[controller.ControllerType];

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
				controller.Filters.Add(filter(provider));
			}

			controller.Selectors.Clear();
			controller.Selectors.Add(model);
		}
	}
}