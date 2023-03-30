using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OrderManagementWebAPI.DTOs.CreateUpdateObjects;
using OrderManagementWebAPI.Helpers;
using OrderManagementWebAPI.Model;
using OrderManagementWebAPI.Services.OrderTracesService;

namespace OrderManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTracesController : ControllerBase
    {
        private readonly IOrderTracesService _orderTracesService;
        private readonly ILogger<OrderTracesController> _logger;
        public OrderTracesController(IOrderTracesService orderTracesService, ILogger<OrderTracesController> logger)
        {
            _orderTracesService = orderTracesService;
            _logger = logger;

        }
        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetOrderTracesAsync([FromRoute]int orderNumber)
        {
            string methodName = "GetOrderTracesAsync";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                var labels = await _orderTracesService.GetOrderTracesAsync(orderNumber);
                return labels == null || !labels.Any() ? NotFound(ErrorMessagesEnum.NoElementFound) : Ok(labels);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("{orderNumber}")]
        public async Task<IActionResult> AddOrderTracess([FromRoute] int orderNumber)
        {
            string methodName = "AddOrderTracess";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);

                if (orderNumber == 0)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                await _orderTracesService.AddOrderTracesAsync(orderNumber);
                return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{orderNumber}")]
        public async Task<IActionResult> DeleteOrderTraces([FromRoute] int orderNumber)
        {
            string methodName = "DeleteOrderTraces";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                bool result = await _orderTracesService.DeleteOrderTracesAsync(orderNumber);
                return result ? Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted) : BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{idBoxNumber}")]
        public async Task<IActionResult> PatchOrderTrace([FromRoute] string idBoxNumber, [FromBody] CreateUpdateOrderTraces orderTrace)
        {
            string methodName = "PatchOrderTrace";
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                if (orderTrace == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateOrderTraces updatedOrderTrace = await _orderTracesService.UpdatePartiallyOrderTracesAsync(idBoxNumber, orderTrace);
                return updatedOrderTrace == null
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
    }
}
