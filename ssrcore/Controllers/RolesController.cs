
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using ssrcore.Repositories;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var result = await _roleRepository.CreateRole(model);
            await _roleRepository.Save();
            return Created("", result);
        }
    }
}