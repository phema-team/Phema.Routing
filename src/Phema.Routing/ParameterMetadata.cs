using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phema.Routing
{
	internal sealed class ParameterMetadata
	{
		public ParameterMetadata(BindingSource bindingSource)
		{
			BindingSource = bindingSource;
		}
		
		public BindingSource BindingSource { get; }
	}
}