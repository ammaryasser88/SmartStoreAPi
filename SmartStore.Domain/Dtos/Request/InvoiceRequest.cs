using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Request
{
    public class InvoiceRequest
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public Decimal PaidAmount { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public decimal TotalAmount { get; set; }
        public SafeTransaction? SafeTransaction { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    }
}
