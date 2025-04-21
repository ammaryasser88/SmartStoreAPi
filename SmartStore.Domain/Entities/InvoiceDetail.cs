using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => (Quantity * UnitPrice) - Discount;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Invoice Invoice { get; set; }
        public Item Item { get; set; }
    }
}
