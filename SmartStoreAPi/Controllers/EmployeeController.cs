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
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _service;
        private readonly IMessageService _messageService;

        public EmployeeController(IEmployeeService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] EmployeeRequest request)
        {
            var response = await _service.AddEmployeeAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginationObject<EmployeeResponse>>> GetAll([FromQuery] int pageIndex = 1)
        {
            var response = await _service.GetEmployeesAsync(pageIndex);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound(_messageService.GetMessage("ValueNotFound"));
        }


        [HttpGet("GetById")]
        public async Task<ActionResult> GetById([FromQuery] int employeeId)
        {
            var response = await _service.GetEmployeeByIdAsync(employeeId);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] int employeeId, [FromBody] EmployeeRequest request)
        {
            var response = await _service.UpdateEmployeeAsync(employeeId, request);
            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return NotFound(new { Message = response.message });
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int itemId)
        {
            var response = await _service.DeleteEmployeeAsync(itemId);

            if (response.result)
                return Ok(new { Message = response.message });
            return NotFound(new { Message = response.message });
        }


        [HttpGet("search")]
        public async Task<ActionResult<PaginationObject<EmployeeResponse>>> Search([FromQuery] string input, [FromQuery] int pageIndex = 1)
        {
            var response = await _service.SearchEmployeeAsync(input, pageIndex);
            if (response != null)
                return Ok(response);

            return NotFound(new { Message = _messageService.GetMessage("ValueNotFound") });
        }
    }
}
