using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Services.BusinessServices.Abstraction
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> AddSaleInvoiceAsync(InvoiceRequest request);
        Task<InvoiceResponse> AddPurchaseInvoiceAsync(InvoiceRequest request);
        Task<InvoiceResponse> AddSalesReturnInvoiceAsync(InvoiceRequest request);
        Task<InvoiceResponse> AddPurchaseReturnInvoiceAsync(InvoiceRequest request);
    }
}
