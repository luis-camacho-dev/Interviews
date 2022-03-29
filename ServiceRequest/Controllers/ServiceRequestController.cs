using Common;
using ILogic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceRequest.Controllers
{
    [Route("api/servicerequest")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        // GET: api/<ServiceRequestController>

        private readonly IServiceRequestManager _serviceRequestManager;
        public ServiceRequestController(IServiceRequestManager serviceRequest)
        {
            _serviceRequestManager = serviceRequest;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<ServiceRequestDTO>> Get()
        {
            IEnumerable< ServiceRequestDTO> serviceRequests = _serviceRequestManager.GetAll();
            if (serviceRequests != null && serviceRequests.Any())
            {
                return Ok(serviceRequests);
            }
            return NotFound("There is not service Requets in the database.");
        }

        // GET api/<ServiceRequestController>/5
        [HttpGet("{id}")]
        public ActionResult<ServiceRequestDTO> Get(Guid id)
        {
            var serviceRequest = _serviceRequestManager.Get(id); ;
            if (serviceRequest != null)
            {
                return serviceRequest;
            }
            return NotFound($"There is not service record for the Identifier={id}");
        }

        // POST api/<ServiceRequestController>
        [HttpPost]
        public ActionResult<Guid> Post([FromBody] ServiceRequestDTO value)
        {
            try
            {
                _serviceRequestManager.Create(value);
                return value.Id;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // PUT api/<ServiceRequestController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]ServiceRequestDTO value, Guid id)
        {
            try
            {
                value.Id = id;
                _serviceRequestManager.Update(value);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ServiceRequestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _serviceRequestManager.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
