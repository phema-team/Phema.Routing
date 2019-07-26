using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Routing.EndpointExample
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddRouting(routing =>
					routing.AddController<OrdersController>("orders", controller =>
					{
						controller.AddRoute("{id}", c => c.GetById(From.Route<int>("id")))
							.HttpGet();

						controller.AddRoute(c => c.Create(From.Body<OrderModel>()))
							.HttpPost();
					}));
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}