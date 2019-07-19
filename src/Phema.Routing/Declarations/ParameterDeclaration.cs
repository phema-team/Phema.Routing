using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phema.Routing
{
	internal sealed class ParameterDeclaration
	{
		public ParameterDeclaration(BindingSource bindingSource, string modelName = null)
		{
			BindingSource = bindingSource;
			ModelName = modelName;
		}

		public BindingSource BindingSource { get; }
		public string ModelName { get; }
	}
}