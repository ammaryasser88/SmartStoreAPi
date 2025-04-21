using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class StoreItemQuantity
    {

        public int StoreId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Store Store { get; set; }
        public Item Item { get; set; }
    }
}
