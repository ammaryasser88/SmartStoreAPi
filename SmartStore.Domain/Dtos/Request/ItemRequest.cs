using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Request
{
    public class ItemRequest
    {
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string Barcode { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public int UnitId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Stock { get; set; }
        public bool? IsActive { get; set; }
    }
}
