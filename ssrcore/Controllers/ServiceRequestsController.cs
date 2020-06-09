using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Repositories;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly IMapper _mapper;

        public ServiceRequestsController(IServiceRequestRepository serviceRequestRepository, IMapper mapper)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServiceRequest(SearchServiceRequestModel model)
        {
            var serviceRequests = await _serviceRequestRepository.GetAllServiceRequests(model);
            return Ok(
                new
                {
                    data = serviceRequests,
                    totalCount = serviceRequests.TotalCount,
                    totalPages = serviceRequests.TotalPages
                });
        }

        [HttpGet("{ticketId}", Name = "GetServiceRequest")]
        public async Task<IActionResult> GetServiceRequest(string ticketId)
        {
            var serivceRequest = await _serviceRequestRepository.GetServiceRequest(ticketId);
            if (serivceRequest == null)
            {
                return NotFound();
            }
            return Ok(serivceRequest);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServiceRequest(string ticketId, ServiceRequestModel model)
        {
            var serivceRequest = await _serviceRequestRepository.GetServiceRequest(ticketId);
            if (serivceRequest == null)
            {
                return NotFound();
            }

            _mapper.Map(model, serivceRequest);
            _serviceRequestRepository.Update(model);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveServiceRequest(string ticketId)
        {
            var result = await _serviceRequestRepository.Remove(ticketId);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }


    }
}