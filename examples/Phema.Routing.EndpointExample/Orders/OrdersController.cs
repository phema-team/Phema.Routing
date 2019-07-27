using Microsoft.AspNetCore.Mvc;

namespace Phema.Routing.EndpointExample
{
	public class OrdersController : ControllerBase
	{
		public OrderModel GetById(int orderId)
		{
			return new OrderModel
			{
				Name = $"Order with id '{orderId}'",
				Cost = 0xCA11AB1E
			};
		}

		public IActionResult Create(OrderModel model)
		{
			var action = Url.Action<OrdersController>(c => c.Create(From.Body<OrderModel>()));
			
			return Created(action, model);
		}
	}
}