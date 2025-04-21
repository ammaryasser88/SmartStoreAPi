using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Repository.Implementation;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Shared.Pagination;

namespace SmartStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryService _service;
        private readonly IMessageService _messageService;

        public ItemCategoryController(IItemCategoryService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }


        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] ItemCategoryRequest request)
        {
            var response = await _service.AddItemCategoryAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginationObject<ItemCategoryResponse>>> GetAll([FromQuery] int pageIndex = 1)
        {
            var response = await _service.GetItemsCategoriesAsync(pageIndex);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound(_messageService.GetMessage("ValueNotFound"));
        }


        [HttpGet("GetById")]
        public async Task<ActionResult> GetById([FromQuery] int itemCategoryId)
        {
            var response = await _service.GetItemCategoryByIdAsync(itemCategoryId);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }


        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromQuery] int itemCategoryId, [FromBody] ItemCategoryRequest request)
        {
            var response = await _service.UpdateItemCategoryAsync(itemCategoryId, request);
            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return NotFound(new { Message = response.message });
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete([FromQuery] int itemCategoryId)
        {
            var response = await _service.DeleteItemCategoryAsync(itemCategoryId);

            if (response.result)
                return Ok(new { Message = response.message });
            return NotFound(new { Message = response.message });
        }


        [HttpGet("search")]
        public async Task<ActionResult<PaginationObject<ItemCategoryResponse>>> Search([FromQuery] string input, [FromQuery] int pageIndex = 1)
        {
            var response = await _service.SearchItemCategoryAsync(input, pageIndex);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }
    }
}
