using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class SafeTransaction
    {
        public int TransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public int SafeId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Safe Safe { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
