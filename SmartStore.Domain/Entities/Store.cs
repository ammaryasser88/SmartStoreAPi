using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<StoreItemQuantity> StoreItemQuantities { get; set; } = new List<StoreItemQuantity>();
        public ICollection<StockAdjustment> StockAdjustments { get; set; } = new List<StockAdjustment>();
        public ICollection<DamagedItem> DamagedItems { get; set; } = new List<DamagedItem>();
    }
}
