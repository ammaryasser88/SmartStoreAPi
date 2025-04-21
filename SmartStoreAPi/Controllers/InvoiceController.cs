using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Services.ApplicationServices.Abstraction;
using SmartStore.Application.Services.BusinessServices.Abstraction;
using SmartStore.Domain.Dtos.Request;

namespace SmartStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;
        private readonly IMessageService _messageService;

        public InvoiceController(IInvoiceService service, IMessageService messageservice)
        {
            _service = service;
            _messageService = messageservice;
        }

        [HttpPost("SaleInvoice")]
        public async Task<IActionResult> AddSaleInvoice([FromBody] InvoiceRequest request)
        {
            var response = await _service.AddSaleInvoiceAsync(request);

            if (response!=null)
            {
                return Ok(response);
            }

            return BadRequest(new { Message = _messageService.GetMessage("FailedToRegisterInvoice") });
        }
        [HttpPost("SalesReturnInvoice")]
        public async Task<IActionResult> AddSalesReturnInvoice([FromBody] InvoiceRequest request)
        {
            var response = await _service.AddSalesReturnInvoiceAsync(request);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(new { Message = _messageService.GetMessage("FailedToRegisterInvoice") });
        }
        [HttpPost("PurchaseInvoice")]
        public async Task<IActionResult> AddPurchaseInvoice([FromBody] InvoiceRequest request)
        {
            var response = await _service.AddPurchaseInvoiceAsync(request);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(new { Message = _messageService.GetMessage("FailedToRegisterInvoice") });
        }
        [HttpPost("PurchaseReturnInvoice")]
        public async Task<IActionResult> AddPurchaseReturnInvoice([FromBody] InvoiceRequest request)
        {
            var response = await _service.AddPurchaseReturnInvoiceAsync(request);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest(new { Message = _messageService.GetMessage("FailedToRegisterInvoice") });
        }
    }
}
