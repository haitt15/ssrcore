using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Services;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments([FromQuery] SearchDepartmentModel model)
        {
            var result = await _departmentService.GetAllDepartment(model);
            return Ok(result);
        }

        [HttpGet("{departmentId}", Name = "GetDepartment")]
        public async Task<IActionResult> GetDepartment(string departmentId)
        {
            var department = await _departmentService.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentModel model)
        {
            var result = await _departmentService.CreateDepartment(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> UpdateDepartment(string departmentId, [FromBody] DepartmentModel model)
        {
            var department = await _departmentService.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _departmentService.UpdateDepartment(departmentId, model);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> RemoveDepartment(string departmentId)
        {
            var result = await _departmentService.DeleteDepartment(departmentId);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }

    }
}