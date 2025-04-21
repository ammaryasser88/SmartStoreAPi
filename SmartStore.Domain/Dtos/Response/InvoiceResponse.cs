using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Response
{
    public class InvoiceResponse:InvoiceRequest
    {
        public int InvoiceId { get; set; }
        public ICollection<InvoiceDetailResponse> Details { get; set; } = new List<InvoiceDetailResponse>();
        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public object RemainingAmount { get; set; }
    }
}
