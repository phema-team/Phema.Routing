using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	internal sealed class PhemaRoutingMvcOptionsPostConfiguration : IPostConfigureOptions<MvcOptions>
	{
		private readonly PhemaRoutingConfigurationOptions options;
		private readonly IServiceProvider provider;

		public PhemaRoutingMvcOptionsPostConfiguration(IServiceProvider provider, IOptions<PhemaRoutingConfigurationOptions> options)
		{
			this.provider = provider;
			this.options = options.Value;
		}

		public void PostConfigure(string name, MvcOptions mvcOptions)
		{
			mvcOptions.Conventions.Add(new ControllerModelConvention(provider, options));
			mvcOptions.Conventions.Add(new ActionModelConvention(provider, options));
			mvcOptions.Conventions.Add(new ParameterModelConvention(options));
		}
	}
}