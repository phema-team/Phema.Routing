using Microsoft.AspNetCore.Mvc;

namespace Phema.Routing.EndpointExample
{
	public class OrdersController
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
			return new CreatedResult("orders", model);
		}
	}
}