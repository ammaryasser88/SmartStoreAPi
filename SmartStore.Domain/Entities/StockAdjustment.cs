using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class StockAdjustment
    {
        public int StockAdjustmentId { get; set; }
        public int StoreId { get; set; }
        public int ItemId { get; set; }
        public decimal QuantityBefore { get; set; }
        public decimal QuantityAfter { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Store Store { get; set; }
        public Item Item { get; set; }
        public decimal Difference { get; set; }
    }
}
