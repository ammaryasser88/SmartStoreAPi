using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Domain.Entities;
using SmartStore.Shared.Pagination;

namespace SmartStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemUnitController : ControllerBase
    {
        private readonly IItemUnitService _service;
        private readonly IMessageService _messageService;

        public ItemUnitController(IItemUnitService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ItemUnitRequest request)
        {
            var response = await _service.AddItemUnitAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginationObject<ItemUnitResponse>>> GetAll([FromQuery] int pageIndex = 1)
        {
            var response = await _service.GetItemsUnitsAsync(pageIndex);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound(_messageService.GetMessage("ValueNotFound"));
        }


        [HttpGet("GetById")]
        public async Task<ActionResult> GetById([FromQuery] int itemUnitId)
        {
            var response = await _service.GetItemUnitByIdAsync(itemUnitId);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] int itemUnitId, [FromBody] ItemUnitRequest request)
        {
            var response = await _service.UpdateItemUnitAsync(itemUnitId, request);
            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return NotFound(new { Message = response.message });
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int itemUnitId)
        {
            var response = await _service.DeleteItemUnitAsync(itemUnitId);

            if (response.result)
                return Ok(new { Message = response.message });
            return NotFound(new { Message = response.message });
        }


        [HttpGet("search")]
        public async Task<ActionResult<PaginationObject<ItemUnitResponse>>> Search([FromQuery] string input, [FromQuery] int pageIndex = 1)
        {
            var response = await _service.SearchItemUnitAsync(input, pageIndex);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }
    }
}
