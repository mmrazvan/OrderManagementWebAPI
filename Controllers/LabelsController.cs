using System.Data.Entity.ModelConfiguration;
using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Http;
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
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                var labels = await _labelsService.GetLabelsAsync();
                if (labels == null || !labels.Any())
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return Ok(labels);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methoName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}",Name = "GetLabelByIdAsync")]
        public async Task<IActionResult> GetLabelByIdAsync([FromRoute] int id)
        {
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                var label = await _labelsService.GetLabelByIdAsync(id);
                if (label == null)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return Ok(label);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methoName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{name}",Name = "GetLabelByNameAsync")]
        public async Task<IActionResult> GetLabelByNameAsync([FromRoute] string name)
        {
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                var label = await _labelsService.GetLabelByNameAsync(name);
                if (label == null)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return Ok(label);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methoName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> PostLabel([FromBody] Labels label)
        {
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                if (label == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _labelsService.AddLabelAsync(label);
                return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabelAsync(int id)
        {
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                bool result = await _labelsService.DeleteLabelAsync(id);
                if (result)
                {
                    return Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted);
                }
                return BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{methoName} error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabel([FromRoute]int id, [FromBody]CreateUpdateLabels label)
        {
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                if (label == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateLabels updateLabel = await _labelsService.UpdateLabelsAsync(id, label);
                if (updateLabel == null)
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchLabel([FromRoute] int id, [FromBody] CreateUpdateLabels label)
        {
            string methoName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"{methoName} started at: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                if (label == null)
                {
                    return BadRequest(ErrorMessagesEnum.NoElementFound);
                }
                CreateUpdateLabels updateLabel = await _labelsService.UpdatePartiallyLabelsAsync(id, label);
                if (updateLabel == null)
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
