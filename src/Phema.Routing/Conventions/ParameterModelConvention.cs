using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Phema.Routing
{
	internal sealed class ParameterModelConvention : IParameterModelConvention
	{
		private readonly PhemaConfigurationOptions options;

		public ParameterModelConvention(PhemaConfigurationOptions options)
		{
			this.options = options;
		}

		public void Apply(ParameterModel parameter)
		{
			if (!options.Parameters.TryGetValue(parameter.ParameterInfo, out var metadata))
				return;
			
			if (parameter.BindingInfo == null)
				parameter.BindingInfo = new BindingInfo();

			parameter.BindingInfo.BindingSource = metadata.BindingSource;
		}
	}
}
