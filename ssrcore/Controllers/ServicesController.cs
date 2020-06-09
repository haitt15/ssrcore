using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Repositories;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServicesController(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices([FromBody] SearchServicModel model)
        {
            var services = await _serviceRepository.GetAllServices(model);
            return Ok(
                new
                {
                    data = services,
                    totalCount = services.TotalCount,
                    totalPages = services.TotalPages
                });
        }

        [HttpGet("{serviceId}", Name = "GetService")]
        public async Task<IActionResult> GetService(string serviceId)
        {
            var serivce = await _serviceRepository.GetService(serviceId);
            if(serivce == null)
            {
                return NotFound();
            }
            return Ok(serivce);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(string serviceId, ServiceModel model)
        {
            var service = await _serviceRepository.GetService(serviceId);
            if (service == null)
            {
                return NotFound();
            }

            _mapper.Map(model, service);
            _serviceRepository.Update(model);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveService(string serviceId)
        {
            var result = await _serviceRepository.Remove(serviceId);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}