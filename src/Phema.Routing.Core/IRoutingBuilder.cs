using System;

namespace Phema.Routing
{
	public interface IRoutingBuilder
	{
		IRouteBuilder AddController<TController>(string template, Action<IControllerBuilder<TController>> controller);
	}
}