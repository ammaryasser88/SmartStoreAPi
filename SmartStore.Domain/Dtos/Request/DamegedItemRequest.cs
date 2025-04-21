using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Request
{
    public class DamegedItemRequest
    {
        public int ItemId { get; set; }
        public int StoreId { get; set; }
        public decimal Quantity { get; set; }
        public string? Reason { get; set; }
        public DateTime Date { get; set; }
        public bool? IsActive { get; set; }
    }
}
