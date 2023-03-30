using System.Data.Entity.ModelConfiguration;
using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.DTOs.CreateUpdateObjects;
using OrderManagementWebAPI.Helpers;
using OrderManagementWebAPI.Services.LabelsService;

namespace OrderManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelsService _labelsService;
        private readonly ILogger<LabelsController> _logger;
        public LabelsController(ILabelsService labelsService, ILogger<LabelsController> logger)
        {
            _labelsService = labelsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetLabelsAsync()
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}",methodName, DateTime.Now);
                var labels = await _labelsService.GetLabelsAsync();
                return labels == null || !labels.Any() ? NotFound(ErrorMessagesEnum.NoElementFound) : Ok(labels);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}",Name = "GetLabelByIdAsync")]
        public async Task<IActionResult> GetLabelByIdAsync([FromRoute] int id)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                var label = await _labelsService.GetLabelByIdAsync(id);
                return label == null ? NotFound(ErrorMessagesEnum.NoElementFound) : Ok(label);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{name}",Name = "GetLabelByNameAsync")]
        public async Task<IActionResult> GetLabelByNameAsync([FromRoute] string name)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                var label = await _labelsService.GetLabelByNameAsync(name);
                return label == null ? NotFound(ErrorMessagesEnum.NoElementFound) : Ok(label);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }        

        [HttpPost]
        public async Task<IActionResult> AddLabel([FromBody] Labels label)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                if (label == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _labelsService.AddLabelAsync(label);
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
        public async Task<IActionResult> DeleteLabelAsync(int id)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                bool result = await _labelsService.DeleteLabelAsync(id);
                return result ? Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted) : BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError("{methodName} error: {Message}", methodName, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabel([FromRoute]int id, [FromBody]CreateUpdateLabels label)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                if (label == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateLabels updatedLabel = await _labelsService.UpdateLabelsAsync(id, label);
                return updatedLabel == null
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
        public async Task<IActionResult> PatchLabel([FromRoute] int id, [FromBody] CreateUpdateLabels label)
        {
            string methodName = MethodBase.GetCurrentMethod()!.Name;
            try
            {
                _logger.LogInformation("{methodName} started at: {Date}", methodName, DateTime.Now);
                if (label == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateLabels updatedLabel = await _labelsService.UpdatePartiallyLabelsAsync(id, label);
                return updatedLabel == null
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
