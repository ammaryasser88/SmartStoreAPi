using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Entities;

namespace SmartStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAdjustmentController : ControllerBase
    {

        private readonly IStockAdjustmentService _service;
        private readonly IMessageService _messageService;

        public StockAdjustmentController(IStockAdjustmentService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] StockAdjustmentRequest request)
        {
            var response = await _service.AddStockAdjustmentAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] int stockAdjustmentId, [FromBody] StockAdjustmentRequest request)
        {
            var response = await _service.UpdateStockAdjustmentAsync(stockAdjustmentId, request);
            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return NotFound(new { Message = response.message });
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int stockAdjustmentId)
        {
            var response = await _service.DeleteStockAdjustmentAsync(stockAdjustmentId);

            if (response.result)
                return Ok(new { Message = response.message });
            return NotFound(new { Message = response.message });
        }
    }
}
