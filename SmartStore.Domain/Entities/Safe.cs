using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartStore.Domain.Entities
{
    public class Safe : IHasRowVersion
    {
        public int SafeId { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public decimal Balance { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = default!;

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<SafeTransaction> SafeTransactions { get; set; } = new List<SafeTransaction>();
    }
}
