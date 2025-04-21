using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class ItemType
    {
        public int ItemTypeId { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
