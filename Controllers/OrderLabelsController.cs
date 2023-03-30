using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;

using OrderManagementWebAPI.Helpers;
using OrderManagementWebAPI.Services.OrderLabelsService;

namespace OrderManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLabelsController : ControllerBase
    {
        private readonly IOrderLabelsService _orderLabelsService;
        private readonly ILogger<OrderLabelsController> _logger;
        public OrderLabelsController(IOrderLabelsService orderLabelsService, ILogger<OrderLabelsController> logger)
        {
            _orderLabelsService = orderLabelsService;
            _logger = logger;
        }

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> GetOrderLabelsAsync([FromRoute]int orderNumber)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                var orderLabels = await _orderLabelsService.GetOrderLabelsAsync(orderNumber);
                return orderLabels == null ? NotFound(ErrorMessagesEnum.NoElementFound) : Ok(orderLabels);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost("{orderNumber}")]
        public async Task<IActionResult> AddOrderLabels([FromRoute] int orderNumber)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);

                if (orderNumber == 0)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                await _orderLabelsService.AddOrderLabels(orderNumber);
                return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{orderNumber}")]
        public async Task<IActionResult> DeleteOrderLabels([FromRoute] int orderNumber)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                bool result = await _orderLabelsService.DeleteOrderLabelsAsync(orderNumber);
                return result ? Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted) : BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
