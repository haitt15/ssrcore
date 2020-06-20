using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Helpers;
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
        public async Task<IActionResult> GetAllServiceRequest([FromQuery]SearchServiceRequestModel model)
        {
            var serviceRequests = await _serviceRequestRepository.GetAllServiceRequests(model);
            dynamic result;

            List<Dictionary<string, object>> listModel = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(model.Fields))
            {
                string[] filter = model.Fields.Split(",");
                foreach (var s in serviceRequests)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < filter.Length; i++)
                    {
                        switch (filter[i].Trim())
                        {
                            case "TicketId":
                                dictionary.Add("TicketId", s.TicketId);
                                break;
                            case "User":
                                dictionary.Add("User", s.User);
                                break;
                            case "UserId":
                                dictionary.Add("UserId", s.UserId);
                                break;
                            case "ServiceId":
                                dictionary.Add("ServiceId", s.ServiceId);
                                break;
                            case "ServiceNm":
                                dictionary.Add("ServiceNm", s.ServiceNm);
                                break;
                            case "StaffId":
                                dictionary.Add("StaffId", s.StaffId);
                                break;
                            case "StaffNm":
                                dictionary.Add("StaffNm", s.Staff);
                                break;
                            case "Content":
                                dictionary.Add("Content", s.Content);
                                break;
                            case "DueDateTime":
                                dictionary.Add("DueDateTime", s.DueDateTime);
                                break;
                            case "Status":
                                dictionary.Add("Status", s.Status);
                                break;
                        }
                    }
                    listModel.Add(dictionary);
                }
                result = listModel;
            }
            else
            {
                result = serviceRequests;
            }
            return Ok(
                new
                {
                    data = result,
                    totalCount = serviceRequests.TotalCount,
                    totalPages = serviceRequests.TotalPages
                });
        }

        [HttpGet("{ticketId}", Name = "GetServiceRequest")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetServiceRequest(string ticketId)
        {
            var serivceRequest = await _serviceRequestRepository.GetServiceRequest(ticketId);
            if (serivceRequest == null)
            {
                return NotFound();
            }
            return Ok(serivceRequest);
        }

        [HttpPost]

        public async Task<IActionResult> CreateServiceRequest([FromBody] ServiceRequestModel model)
        {
            var result = await _serviceRequestRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
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