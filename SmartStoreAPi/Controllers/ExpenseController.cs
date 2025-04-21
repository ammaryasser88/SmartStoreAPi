using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Domain.Dtos.Request;

namespace SmartStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {

        private readonly IExpenseService _service;
        private readonly IMessageService _messageService;

        public ExpenseController(IExpenseService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("AddExpense")]
        public async Task<IActionResult> Add([FromBody] ExpenseRequest request)
        {
            var response = await _service.AddExpenseAsync(request);

            if (response.result)
            {
                return Ok(new { Message = response.message });
            }

            return BadRequest(new { Message = response.message });
        }
    }
}
