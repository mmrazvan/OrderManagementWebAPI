using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OrderManagementWebAPI.Helpers;
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
            string methodName = MethodBase.GetCurrentMethod()!.Name;
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
    }
}
