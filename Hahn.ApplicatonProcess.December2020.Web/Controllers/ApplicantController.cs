using Hahn.ApplicatonProcess.December2020.Domain.DomainModel;
using Hahn.ApplicatonProcess.December2020.Domain.Helpers;
using Hahn.ApplicatonProcess.December2020.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ApplicantController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IApplicantService _IApplicantService;
        public ApplicantController(ILogger<ApplicantController> logger, IApplicantService IApplicantService)
        {
            _logger = logger;
            _IApplicantService = IApplicantService;
        }

        [HttpPost("Insert")]
        [ProducesResponseType(typeof(EntityResponseModel<ApplicantModel>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Insert(ApplicantModel model)
        {
            _logger.LogInformation($"Isert Called {DateTimeOffset.UtcNow}");
            var returnResponse = new EntityResponseModel<ApplicantModel>();
            try
            {
                var result = await _IApplicantService.Insert(model);
                if (result.ReturnStatus == false)
                    return StatusCode((int)HttpStatusCode.BadRequest, JsonConvert.SerializeObject(result));
                return StatusCode((int)HttpStatusCode.Created, JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(JsonConvert.SerializeObject(returnResponse));
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(EntityResponseModel<ApplicantModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"GetById Called {DateTimeOffset.UtcNow}");
            EntityResponseModel<ApplicantModel> returnResponse = new EntityResponseModel<ApplicantModel>();
            try
            {
                var result = await _IApplicantService.GetById(id);
                if (result.ReturnStatus == false)
                    return BadRequest(JsonConvert.SerializeObject(result));
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(JsonConvert.SerializeObject(returnResponse));
            }
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(EntityResponseListModel<ApplicantModel>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"GetAll Called {DateTimeOffset.UtcNow}");
            EntityResponseListModel<ApplicantModel> returnResponse = new EntityResponseListModel<ApplicantModel>();
            try
            {
                var result = await _IApplicantService.GetAll();
                if (result.ReturnStatus == false)
                    return BadRequest(JsonConvert.SerializeObject(result));
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(JsonConvert.SerializeObject(returnResponse));
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(EntityResponseModel<ApplicantModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(ApplicantModel model)
        {
            _logger.LogInformation($"Update Called {DateTimeOffset.UtcNow}");

            var returnResponse = new EntityResponseModel<ApplicantModel>();
            try
            {
                var result = await _IApplicantService.Update(model);
                if (result.ReturnStatus == false)
                    return BadRequest(JsonConvert.SerializeObject(result));
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(JsonConvert.SerializeObject(returnResponse));
            }
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(EntityResponseModel<ApplicantModel>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Delete(int Id)
        {
            _logger.LogInformation($"Delete Called {DateTimeOffset.UtcNow}");

            var returnResponse = new EntityResponseModel<ApplicantModel>();
            try
            {
                var result = await _IApplicantService.Delete(Id);
                if (result.ReturnStatus == false)
                    return BadRequest(JsonConvert.SerializeObject(result));
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(JsonConvert.SerializeObject(returnResponse));
            }
        }
    }
}
