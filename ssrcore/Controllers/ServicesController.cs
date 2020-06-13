using System.Collections.Generic;
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
        public async Task<IActionResult> GetAllServices([FromQuery] SearchServicModel model)
        {
            var services = await _serviceRepository.GetAllServices(model);
            dynamic result;

            List<Dictionary<string, object>> listModel = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(model.Fields))
            {
                string[] filter = model.Fields.Split(",");
                foreach (var s in services)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < filter.Length; i++)
                    {
                        switch (filter[i].Trim())
                        {
                            case "ServiceId":
                                dictionary.Add("ServiceId", s.ServiceId);
                                break;
                            case "ServiceNm":
                                dictionary.Add("ServiceNm", s.ServiceNm);
                                break;
                            case "DescriptionService":
                                dictionary.Add("Description Service", s.DescriptionService);
                                break;
                            case "FormLink":
                                dictionary.Add("FormLink", s.FormLink);
                                break;
                            case "SheetLink":
                                dictionary.Add("RoomNum", s.SheetLink);
                                break;
                            case "ProcessMaxDay":
                                dictionary.Add("Process Max Day", s.ProcessMaxDay);
                                break;
                            case "DepartmentId":
                                dictionary.Add("DepartmentId", s.DepartmentId);
                                break;
                            case "DepartmentNm":
                                dictionary.Add("Department Name", s.DepartmentNm);
                                break;
                        }
                    }
                    listModel.Add(dictionary);
                }
                result = listModel;
            }
            else
            {
                result = services;
            }
            return Ok(
                new
                {
                    data = result,
                    totalCount = services.TotalCount,
                    totalPages = services.TotalPages
                });
        }

        [HttpGet("{serviceId}", Name = "GetService")]
        public async Task<IActionResult> GetService(string serviceId)
        {
            var serivce = await _serviceRepository.GetService(serviceId);
            if (serivce == null)
            {
                return NotFound();
            }
            return Ok(serivce);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceModel model)
        {
            var result = await _serviceRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
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