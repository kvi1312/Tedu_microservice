using System.ComponentModel.DataAnnotations;
using Contracts.Sagas.OrderManager;
using Microsoft.AspNetCore.Mvc;
using Saga.Orchestrator.OrderManager;
using Shared.DTOs.Basket;

namespace Saga.Orchestrator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly ISagaOrderManager<BasketCheckoutDto, OrderResponse> _sagaOrderManager;

    public CheckoutController(ISagaOrderManager<BasketCheckoutDto, OrderResponse> sagaOrderManager)
    {
        _sagaOrderManager = sagaOrderManager;
    }

    [HttpPost]
    [Route("{username}")]
    public OrderResponse CheckoutOrder([Required] string username,
        [FromBody] BasketCheckoutDto model)
    {
        model.SetUserName(username);
        var result = _sagaOrderManager.CreateOrder(model);
        return result;
    }
}