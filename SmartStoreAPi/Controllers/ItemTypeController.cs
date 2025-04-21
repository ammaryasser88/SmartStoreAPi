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
    public class ItemTypeController : ControllerBase
    {
        private readonly IItemTypeService _service;
        private readonly IMessageService _messageService;

        public ItemTypeController(IItemTypeService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ItemTypeRequest request)
        {
            var response = await _service.AddItemTypeAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginationObject<ItemTypeResponse>>> GetAll([FromQuery] int pageIndex = 1)
        {
            var response = await _service.GetItemsTypesAsync(pageIndex);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound(_messageService.GetMessage("ValueNotFound"));
        }


        [HttpGet("GetById")]
        public async Task<ActionResult> GetById([FromQuery] int itemTypeId)
        {
            var response = await _service.GetItemTypeByIdAsync(itemTypeId);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] int itemTypeId, [FromBody] ItemTypeRequest request)
        {
            var response = await _service.UpdateItemTypeAsync(itemTypeId, request);
            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return NotFound(new { Message = response.message });
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int itemTypeId)
        {
            var response = await _service.DeleteItemTypeAsync(itemTypeId);

            if (response.result)
                return Ok(new { Message = response.message });
            return NotFound(new { Message = response.message });
        }


        [HttpGet("search")]
        public async Task<ActionResult<PaginationObject<ItemTypeResponse>>> Search([FromQuery] string input, [FromQuery] int pageIndex = 1)
        {
            var response = await _service.SearchItemTypeAsync(input, pageIndex);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }
    }
}
