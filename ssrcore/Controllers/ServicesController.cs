using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Repositories;
using ssrcore.Services;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServicesController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices([FromQuery] SearchServicModel model)
        {
            var result = await _serviceService.GetAllService(model);
            return Ok(result);
        }

        [HttpGet("{serviceId}", Name = "GetService")]
        public async Task<IActionResult> GetService(string serviceId)
        {
            var serivce = await _serviceService.GetService(serviceId);
            if (serivce == null)
            {
                return NotFound();
            }
            return Ok(serivce);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceModel model)
        {
            var result = await _serviceService.CreateService(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{serviceId}")]
        public async Task<IActionResult> UpdateService(string serviceId, [FromBody] ServiceModel model)
        {
            var service = await _serviceService.GetService(serviceId);
            if (service == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _serviceService.UpdateService(serviceId, model);
                return Ok(result);
            }
            return BadRequest();
        }
        
        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> DeleteService(string serviceId)
        {
            var result = await _serviceService.DeleteService(serviceId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}