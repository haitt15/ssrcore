using Microsoft.AspNetCore.Mvc;
using ssrcore.Services;
using System.Threading.Tasks;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestHistoryController : ControllerBase
    {
        private readonly IRequestHistoryService _requestHistoryService;

        public RequestHistoryController(IRequestHistoryService requestHistoryService)
        {
            _requestHistoryService = requestHistoryService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetRequestHistoryByTicketId(string ticketId)
        {
            var result = await _requestHistoryService.GetRequestHistory(ticketId);
            return Ok(result);
        }


    }
}