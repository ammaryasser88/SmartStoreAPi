using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string Barcode { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public int UnitId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Stock { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ItemCategory Category { get; set; }
        public ItemType Type { get; set; }
        public ItemUnit Unit { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public ICollection<StoreItemQuantity> StoreItemQuantities { get; set; } = new List<StoreItemQuantity>();
        public ICollection<StockAdjustment> StockAdjustments { get; set; } = new List<StockAdjustment>();
        public ICollection<DamagedItem> DamagedItems { get; set; } = new List<DamagedItem>();
    }
}
