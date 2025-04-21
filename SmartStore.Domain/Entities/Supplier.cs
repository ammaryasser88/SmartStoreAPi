using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
