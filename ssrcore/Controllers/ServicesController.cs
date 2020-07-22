using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Helpers;
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
        private readonly IRedisCacheRepository _redisCacheRepository;
        private readonly IMapper _mapper;

        public ServicesController(IServiceService serviceService, IMapper mapper, IRedisCacheRepository redisCacheRepository)
        {
            _serviceService = serviceService;
            _redisCacheRepository = redisCacheRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices([FromQuery] SearchServicModel model)
        {
            var isCache = _redisCacheRepository.isExist(Constants.KeyRedis.SERVICES);
            if (!isCache)
            {
                var listServices = await _serviceService.GetServices();
                _redisCacheRepository.Add(Constants.KeyRedis.SERVICES, listServices);
            }
            var cacheResult = _redisCacheRepository.Get<IEnumerable<ServiceModel>>(Constants.KeyRedis.SERVICES);
            var result = _serviceService.GetAllService(model, cacheResult);
            return Ok(result);
        }

        [HttpGet("{serviceId}", Name = "GetService")]
        public async Task<IActionResult> GetService(string serviceId)
        {
            var isCache = _redisCacheRepository.isExist(Constants.KeyRedis.SERVICES);
            if (!isCache)
            {
                var listServices = await _serviceService.GetServices();
                _redisCacheRepository.Add(Constants.KeyRedis.SERVICES, listServices);
            }
            var cacheResult = _redisCacheRepository.Get<IEnumerable<ServiceModel>>(Constants.KeyRedis.SERVICES);
            var serivce = _serviceService.GetServiceByRedis(serviceId, cacheResult);
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
                var isCache = _redisCacheRepository.isExist(Constants.KeyRedis.SERVICES);
                if (!isCache)
                {
                    _redisCacheRepository.Delete(Constants.KeyRedis.SERVICES);
                }
                var listServices = await _serviceService.GetServices();
                _redisCacheRepository.Add(Constants.KeyRedis.SERVICES, listServices);
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
                var isCache = _redisCacheRepository.isExist(Constants.KeyRedis.SERVICES);
                if (!isCache)
                {
                    _redisCacheRepository.Delete(Constants.KeyRedis.SERVICES);
                }
                var listServices = await _serviceService.GetServices();
                _redisCacheRepository.Add(Constants.KeyRedis.SERVICES, listServices);
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
                var isCache = _redisCacheRepository.isExist(Constants.KeyRedis.SERVICES);
                if (!isCache)
                {
                    _redisCacheRepository.Delete(Constants.KeyRedis.SERVICES);
                }
                var listServices = await _serviceService.GetServices();
                _redisCacheRepository.Add(Constants.KeyRedis.SERVICES, listServices);
                return NoContent();
            }
            return BadRequest();
        }
    }
}