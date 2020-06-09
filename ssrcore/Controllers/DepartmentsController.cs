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
        public async Task<IActionResult> GetAllDepartments([FromBody] SearchDepartmentModel model)
        {
            var departments = await _departmentRepository.GetAllDepartments(model);
            return Ok(
                new
                {
                    data = departments,
                    totalCount = departments.TotalCount,
                    totalPages = departments.TotalPages 
                });
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartment([FromQuery] string departmentId)
        {
            var department = await _departmentRepository.GetDepartment(departmentId);
            if(departmentId == null)
            {
                return BadRequest();
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentModel model)
        {
            var result = await _departmentRepository.Create(model);
            if (result)
            {
                return CreatedAtRoute("GetAllDepartments", new { departmemtId = model.DepartmentId }, model);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromQuery] string departmentId, [FromBody] DepartmentModel model)
        {
            var department = await _departmentRepository.GetDepartment(departmentId);
            if(department == null)
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