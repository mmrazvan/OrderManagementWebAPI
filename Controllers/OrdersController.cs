using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;
using OrderManagementWebAPI.Helpers;
using OrderManagementWebAPI.Model;
using OrderManagementWebAPI.Services.OrdersService;

namespace OrderManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrdersService ordersService, ILogger<OrdersController> logger)
        {
            _ordersService = ordersService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            string methodName = "GetOrdersAsync";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                var orders = await _ordersService.GetOrdersAsync();
                return orders == null || !orders.Any() ? NotFound(ErrorMessagesEnum.NoElementFound) : Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
        {
            string methodName = "GetOrderByIdAsync";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                Orders order = await _ordersService.GetOrderByIdAsync(id);
                return order == null ? NotFound(ErrorMessagesEnum.NoElementFound) : Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Orders order)
        {
            string methodName = "PostOrder";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                if (order == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _ordersService.AddOrderAsync(order);
                return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            string methodName = "DeleteOrderAsync";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                bool result = await _ordersService.DeleteOrderAsync(id);
                return result ? Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted) : BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] CreateUpdateOrders order)
        {
            string methodName = "PutOrder";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                if (order == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateOrders updatedOrder = await _ordersService.UpdateOrderAsync(id, order);
                return updatedOrder == null
                    ? StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.NoElementFound)
                    : (IActionResult)Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrder([FromRoute] int id, [FromBody] CreateUpdateOrders order)
        {
            string methodName = "PatchOrder";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                if (order == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateOrders updatedOrder = await _ordersService.UpdatePartiallyOrderAsync(id, order);
                return updatedOrder == null ? StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.NoElementFound) : (IActionResult)Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
