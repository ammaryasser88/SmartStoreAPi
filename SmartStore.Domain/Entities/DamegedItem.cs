using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class DamagedItem
    {
        public int DamagedItemId { get; set; }
        public int ItemId { get; set; }
        public int StoreId { get; set; }
        public decimal Quantity { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Item Item { get; set; }
        public Store Store { get; set; }
    }
}
