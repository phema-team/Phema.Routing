using System;
using System.Linq.Expressions;

namespace Phema.Routing
{
	public interface IControllerBuilder<TController>
	{
		IRouteBuilder AddRoute<TResult>(
			string template, 
			Expression<Func<TController, TResult>> expression);
	}
}