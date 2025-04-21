using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class Invoice : IHasRowVersion
    {
        public int InvoiceId { get; set; }
        public int? CustomerId { get; set; }
        public int? SupplierId { get; set; }
        public int StoreId { get; set; }
        public DateTime Date { get; set; }
        public decimal  TotalAmount { get; set; }
        public decimal?  RemainingAmount { get; set; } 
        public decimal  PaidAmount { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; } = default!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public Store store { get; set; }
        public SafeTransaction SafeTransaction { get; set; }
        
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}
