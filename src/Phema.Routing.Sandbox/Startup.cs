using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing.Sandbox
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore()
				.AddJsonFormatters()
				.AddRouting(routing =>
				{
					routing.AddController<Controller>("test",
						controller =>
						{
							controller.AddRoute(c => c.Get(From.Query<string>()))
								.HttpGet()
								.WithName("get");

							controller.AddRoute(c => c.Post(From.Body<int>()))
								.HttpPost()
								.WithName("post");
						});
				});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseMvc();
		}
	}
}