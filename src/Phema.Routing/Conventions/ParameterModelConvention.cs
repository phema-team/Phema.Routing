using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Phema.Routing
{
	internal sealed class ParameterModelConvention : IParameterModelConvention
	{
		private readonly RoutingOptions options;

		public ParameterModelConvention(RoutingOptions options)
		{
			this.options = options;
		}

		public void Apply(ParameterModel parameter)
		{
			var metadata = options.Parameters[parameter.ParameterInfo];

			if (parameter.BindingInfo == null)
			{
				parameter.BindingInfo = new BindingInfo();
			}

			parameter.BindingInfo.BindingSource = metadata.BindingSource;
		}
	}
}
