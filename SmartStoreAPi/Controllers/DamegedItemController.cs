using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Shared.Pagination;

namespace SmartStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamegedItemController : ControllerBase
    {

        private readonly IDamegedItemService _service;
        private readonly IMessageService _messageService;

        public DamegedItemController(IDamegedItemService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] DamegedItemRequest request)
        {
            var response = await _service.AddDamagedItemAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] int damegedItemId, [FromBody] DamegedItemRequest request)
        {
            var response = await _service.UpdateDamagedItemAsync(damegedItemId, request);
            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return NotFound(new { Message = response.message });
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int damegedItemId)
        {
            var response = await _service.DeleteDamagedItemAsync(damegedItemId);

            if (response.result)
                return Ok(new { Message = response.message });
            return NotFound(new { Message = response.message });
        }        
    }
}
