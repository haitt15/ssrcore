using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Repositories;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments([FromQuery] SearchDepartmentModel model)
        {
            dynamic result;
            var departments = await _departmentRepository.GetAllDepartments(model);
     
            List<Dictionary<string, object>> listModel = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(model.Fields))
            {
                string[] filter = model.Fields.Split(",");
                foreach (var m in departments)
                {
                    Dictionary<string, object> dictionnary = new Dictionary<string, object>();
                    for (int i = 0; i < filter.Length; i++)
                    {
                        switch (filter[i].Trim())
                        {
                            case "DepartmentId":
                                dictionnary.Add("DepartmentId", m.DepartmentId);
                                break;
                            case "DepartmentNm":
                                dictionnary.Add("DepartmentNm", m.DepartmentNm);
                                break;
                            case "Hotline":
                                dictionnary.Add("Hotline", m.Hotline);
                                break;
                            case "ManagerId":
                                dictionnary.Add("ManagerId", m.ManagerId);
                                break;
                            case "Manager":
                                dictionnary.Add("Manager", m.Manager);
                                break;
                            case "RoomNum":
                                dictionnary.Add("RoomNum", m.RoomNum);
                                break;
                        }
                    }
                    listModel.Add(dictionnary);
                }
                result = listModel;
            }
            else
            {
                result = departments;
            }
            return Ok(
                new 
                {
                    data = result,
                    totalCount = departments.TotalCount,
                    totalPages = departments.TotalPages
                });
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartment([FromQuery] string departmentId)
        {
            var department = await _departmentRepository.GetDepartment(departmentId);
            if (departmentId == null)
            {
                return BadRequest();
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentModel model)
        {
            var result = await _departmentRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromQuery] string departmentId, [FromBody] DepartmentModel model)
        {
            var department = await _departmentRepository.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound();
            }

            _mapper.Map(model, department);
            _departmentRepository.UpdateDepartment(model);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveDepartment([FromQuery] string departmentId)
        {
            var result = await _departmentRepository.Remove(departmentId);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }

    }
}