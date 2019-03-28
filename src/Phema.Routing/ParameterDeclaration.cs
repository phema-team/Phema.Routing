using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Phema.Routing
{
	internal sealed class ParameterDeclaration
	{
		public ParameterDeclaration(BindingSource bindingSource)
		{
			BindingSource = bindingSource;
		}
		
		public BindingSource BindingSource { get; }
	}
}