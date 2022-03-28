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
        public IEnumerable<ServiceRequestDTO> Get()
        {
            return _serviceRequestManager.GetAll();
        }

        // GET api/<ServiceRequestController>/5
        [HttpGet("{id}")]
        public ServiceRequestDTO Get(Guid id)
        {
            return _serviceRequestManager.Get(id);
        }

        // POST api/<ServiceRequestController>
        [HttpPost]
        public void Post([FromBody] ServiceRequestDTO value)
        {   
            _serviceRequestManager.Create(value);
        }

        // PUT api/<ServiceRequestController>/5
        [HttpPut("{id}")]
        public void Put(ServiceRequestDTO value)
        {
            _serviceRequestManager.Update(value);
        }

        // DELETE api/<ServiceRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _serviceRequestManager.Delete(id);
        }
    }
}
