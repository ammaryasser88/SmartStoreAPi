using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Implementation;
using SmartStore.Domain.Dtos.Request;

namespace SmartStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenueService _service;
        private readonly IMessageService _messageService;

        public RevenueController(RevenueService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("AddRevenue")]
        public async Task<IActionResult> Add([FromBody] RevenueRequest request)
        {
            var response = await _service.AddRevenueAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }
    }
}
