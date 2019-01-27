using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing
{
	internal sealed class MvcOptionsPostConfiguration : IPostConfigureOptions<MvcOptions>
	{
		private readonly RoutingConfigurationOptions options;
		private readonly IServiceProvider provider;

		public MvcOptionsPostConfiguration(IServiceProvider provider, IOptions<RoutingConfigurationOptions> options)
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