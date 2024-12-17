using FacadeDesignPattern.Models;
using FacadeDesignPattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace FacadeDesignPattern.Controllers
{
	public class OrderController : Controller
	{
		private readonly OrderFacade _orderFacade;

		public OrderController(OrderFacade orderFacade)
		{
			_orderFacade = orderFacade;
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Order order)
		{
			if (ModelState.IsValid)
			{
				order.OrderId = new Random().Next(1000, 9999); // Simulated order ID
				_orderFacade.ProcessOrder(order);
				return View("Result", order); // Pass the result to the Result view
			}
			return View(order);
		}
	}


}
