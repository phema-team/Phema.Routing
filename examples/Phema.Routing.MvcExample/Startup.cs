using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing.MvcExample
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(o => o.EnableEndpointRouting = false)
				.AddRouting(routing =>
					routing.AddController<ExampleController>(controller =>
					{
						controller.AddRoute("calculate", c => c.CalculateLength(From.Query<string>()))
							.HttpGet()
							.Produces<int>(200);

						controller.AddRoute("generate", c => c.GenerateString(From.Query<char>(), From.Query<int>()))
							.HttpGet()
							.Produces<string>(200);

						controller.AddRoute("named", c => c.NamedParameter(From.Query<int>("age")))
							.HttpGet();

						controller.AddRoute("named-route/{greet}", c => c.NamedRoute(From.Route<string>("greet")))
							.HttpGet();
					}));
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
		}
	}
}