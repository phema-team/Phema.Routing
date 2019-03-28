using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Phema.Routing;

namespace AspNetCoreExample
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(o => o.OutputFormatters.Add(new XmlSerializerOutputFormatter()))
				.AddRouting(routing =>
					routing.AddController<ExampleController>(controller =>
					{
						controller.AddRoute("calculate", c => c.CalculateLength(From.Query<string>()))
							.HttpGet()
							.Produces<int>(200);

						controller.AddRoute("generate", c => c.GenerateString(From.Query<char>(), From.Query<int>()))
							.HttpGet()
							.Produces<string>(200);
					}));
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
		}
	}
}