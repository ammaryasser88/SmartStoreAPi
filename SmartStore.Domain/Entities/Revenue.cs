using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class Revenue : IHasRowVersion
    {
        public int RevenueId { get; set; }
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; } = default!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
