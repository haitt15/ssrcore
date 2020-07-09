﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Helpers;
using ssrcore.Repositories;
using ssrcore.Services;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{   
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IMapper _mapper;

        public ServiceRequestsController(IServiceRequestService serviceRequestService, IMapper mapper)
        {
            _serviceRequestService = serviceRequestService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServiceRequest([FromQuery]SearchServiceRequestModel model)
        {
            var result = await _serviceRequestService.GetAllServiceRequest(model);
            await _serviceRequestService.UpdateStatusExpiredServiceRequest();
            return Ok(result);
        }

        [HttpGet("{ticketId}", Name = "GetServiceRequest")]
        public async Task<IActionResult> GetServiceRequest(string ticketId)
        {
            var serviceRequest = await _serviceRequestService.GetServiceRequest(ticketId);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            return Ok(serviceRequest);
        }

        [HttpPost]

        public async Task<IActionResult> CreateServiceRequest([FromBody] ServiceRequestModel model)
        {
            var result = await _serviceRequestService.CreateServiceRequest(model);
            if (result != null)
            {
                RequestSheetUtils.Add(result);
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{ticketId}")]
        public async Task<IActionResult> UpdateServiceRequest(string ticketId, ServiceRequestModel model)
        {
            var serivceRequest = await _serviceRequestService.GetServiceRequest(ticketId);
            if (serivceRequest == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _serviceRequestService.UpdateServiceRequest(ticketId, model);
                RequestSheetUtils.Update(result);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("{ticketId}")]
        public async Task<IActionResult> DeleteServiceRequest(string ticketId)
        {
            var result = await _serviceRequestService.DeleteServiceRequest(ticketId);
            if (result)
            {
                RequestSheetUtils.Delete(ticketId);
                return NoContent();
            }
            return BadRequest();
        }


    }
}