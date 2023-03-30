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
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methodName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                var orders = await _ordersService.GetOrdersAsync();
                if (orders == null || !orders.Any())
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methodName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methodName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                Orders order = await _ordersService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methodName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Orders order)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methodName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                if (order == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _ordersService.AddOrderAsync(order);
                return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"{methodName} error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methodName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methodName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                bool result = await _ordersService.DeleteOrderAsync(id);
                if (result)
                {
                    return Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted);
                }
                return BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methodName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] CreateUpdateOrders order)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methodName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                if (order == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateOrders updatedOrder = await _ordersService.UpdateOrderAsync(id, order);
                if (updatedOrder == null)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"{methodName} error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methodName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrder([FromRoute] int id, [FromBody] CreateUpdateOrders order)
        {
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                if (order == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateOrders updatedOrder = await _ordersService.UpdatePartiallyOrderAsync(id, order);
                if (updatedOrder == null)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"{methoName} error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methoName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
