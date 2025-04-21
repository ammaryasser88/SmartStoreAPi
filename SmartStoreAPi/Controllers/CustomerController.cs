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
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _service;
        private readonly IMessageService _messageService;

        public CustomerController(ICustomerService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CustomerRequest request)
        {
            var response = await _service.AddCustomerAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginationObject<CustomerResponse>>> GetAll([FromQuery] int pageIndex = 1)
        {
            var response = await _service.GetCustomersAsync(pageIndex);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound(_messageService.GetMessage("ValueNotFound"));
        }


        [HttpGet("GetById")]
        public async Task<ActionResult> GetById([FromQuery] int customerId)
        {
            var response = await _service.GetCustomerByIdAsync(customerId);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] int customerId, [FromBody] CustomerRequest request)
        {
            var response = await _service.UpdateCustomerAsync(customerId, request);
            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return NotFound(new { Message = response.message });
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int customerId)
        {
            var response = await _service.DeleteCustomerAsync(customerId);

            if (response.result)
                return Ok(new { Message = response.message });
            return NotFound(new { Message = response.message });
        }


        [HttpGet("search")]
        public async Task<ActionResult<PaginationObject<CustomerResponse>>> Search([FromQuery] string input, [FromQuery] int pageIndex = 1)
        {
            var response = await _service.SearchCustomerAsync(input, pageIndex);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }
    }
}
