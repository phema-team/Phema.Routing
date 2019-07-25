using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Phema.Routing
{
	internal sealed class ControllerModelConvention : IControllerModelConvention
	{
		private readonly IServiceProvider provider;
		private readonly RoutingOptions options;

		public ControllerModelConvention(IServiceProvider provider, RoutingOptions options)
		{
			this.options = options;
			this.provider = provider;
		}
		
		public void Apply(ControllerModel controller)
		{
			if (!options.Controllers.TryGetValue(controller.ControllerType, out var metadata))
			{
				return;
			}

			var model = new SelectorModel
			{
				AttributeRouteModel = metadata.Template == null
					? null
					: new AttributeRouteModel(
							new RouteAttribute(metadata.Template)
							{
								Name = metadata.ActionName
							})
			};

			foreach (var constraint in metadata.ActionConstraints)
			{
				model.ActionConstraints.Add(constraint(provider));
			}

			foreach (var filter in metadata.Filters)
			{
				controller.Filters.Add(filter(provider));
			}

			var conventions = controller.Selectors.Where(s => s.AttributeRouteModel == null).ToList();

			foreach (var convention in conventions)
			{
				controller.Selectors.Remove(convention);
			}
			
			controller.Selectors.Insert(0, model);
		}
	}
}